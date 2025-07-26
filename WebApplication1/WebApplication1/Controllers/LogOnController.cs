using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Enum;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class LogOnController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();

        public ActionResult GetLogOnList(int processId)
        {
            var logs = _mes.LogOn
                .Where(l => l.ProcessId == processId)
                .OrderByDescending(l => l.StartTime)
                .ToList();

            return PartialView("~/Views/WorkReport/_LogOnTableRows.cshtml", logs);

        }



        /// <summary>
        /// 檢查工單數量是否超過最大可作業數量
        /// </summary>
        /// <param name="workOrderBarcode">工單條碼</param>
        /// <param name="inputQty">要新增的數量</param>
        /// <returns>Tuple (success, message, exceed, current, max, remain)</returns>
        private (bool success, string message, bool exceed, decimal current, decimal max, decimal remain) CheckWorkOrderQty(string workOrderBarcode, decimal inputQty)
        {
            var wo = _erp.WOes.FirstOrDefault(w => w.WorkOrderBarcode == workOrderBarcode);
            if (wo == null)
            {
                return (false, "找不到對應的工單。", false, 0, 0, 0);
            }

            var logOnQty = _mes.LogOn
                .Where(l => l.WorkOrderBarcode == workOrderBarcode && l.Status != LogOnStatus.Cancel)
                .Sum(l => (decimal?)l.Qty) ?? 0;

            bool exceed = (logOnQty + inputQty) > wo.MaxQuantity;
            decimal remain = wo.MaxQuantity - logOnQty;

            return (true, "", exceed, logOnQty, wo.MaxQuantity, remain);
        }

        [HttpGet]
        public JsonResult CheckQuantity(string workOrderBarcode, decimal qty)
        {
            var check = CheckWorkOrderQty(workOrderBarcode, qty);
            if (!check.success)
                return Json(new { success = false, message = check.message }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                success = true,
                exceed = check.exceed,
                current = check.current,
                max = check.max,
                remain = check.remain
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 開始作業 (LogOn)
        /// </summary>
        [HttpPost]
        public JsonResult StartLogOn(LogOn model)
        {
            if (string.IsNullOrEmpty(model.WorkOrderBarcode))
                return Json(new { success = false, message = "工單條碼不得為空。" });
            try
            {
                // 使用者 (ERP)
                var user = _erp.Users.FirstOrDefault(u => u.WorkId == model.UserWorkId);
                if (user == null)
                    return Json(new { success = false, message = "查無此人" });
                else { 
                    model.UserWorkId = user.WorkId;
                    model.UserName = user.Name;
                    model.OpUserWorkId = user.WorkId;
                    model.OpUserName = user.Name;
                }
                // 工作中心 (ERP)
                var workCenter = _erp.WorkCenters.FirstOrDefault(w => w.Id == model.WorkCenterId);
                if (workCenter == null)
                    return Json(new { success = false, message = "查無工作中心" });
                else
                {
                    model.WorkCenterName = workCenter.Name;
                }
                // 工單 (ERP)
                var workOrder = _erp.WOes.FirstOrDefault(o => o.WorkOrderBarcode == model.WorkOrderBarcode);
                if (workOrder == null)
                    return Json(new { success = false, message = "查無此工單" });
                else
                {
                    model.WorkOrderBarcode = workOrder.WorkOrderBarcode;
                    model.ProductionOrder = workOrder.ProductionOrder;
                    model.MaterialNumber = workOrder.MaterialNumber;
                    model.Route = workOrder.Route;
                    model.ProductionStyle = model.ProductionStyle; 
                }

                // 工序 (MES)
                var process = _mes.Processes.FirstOrDefault(p => p.Id == model.ProcessId);
                if (process == null)
                    return Json(new { success = false, message = "查無工序" });
                else
                {
                    model.ProcessId = process.Id;
                    model.ProcessCode = process.Code;
                    model.ProcessName = process.Name;
                    model.StandardTime = process.StandardTime;
                }
                // 機台 (MES)
                var machine = _mes.Machines.FirstOrDefault(m => m.Id == model.MachineId);
                if (machine == null)
                    return Json(new { success = false, message = "查無機台" });
                else
                {
                    model.MachineId = machine.Id;
                    model.MachineCode = machine.Code;
                    model.NMachineNameCN = machine.NameCN;  // 中文名稱
                    model.MachineNameEN = machine.NameEN;   // 英文名稱
                    model.MachineCategory = machine.Category;
                }

                // 檢查數量
                var check = CheckWorkOrderQty(model.WorkOrderBarcode, model.Qty);
                if (!check.success)
                    return Json(new { success = false, message = check.message });

                if (check.exceed)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"作業數量超過最大可作業數量 ({check.max})，目前已登錄 {check.current}，可用數量 {check.remain}。"
                    });
                }

                // 新增 LogOn
                model.StartTime = DateTime.Now;
                model.Status = LogOnStatus.LogOn;
                _mes.LogOn.Add(model);
                _mes.SaveChanges();

                return Json(new { success = true, message = "作業已開始", logId = model.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "伺服器錯誤：" + ex.Message
            });
            }
        }



        /// <summary>
        /// 結束作業 (LogOff)
        /// </summary>
        [HttpPost]
        public JsonResult EndLogOn(int id)
        {
            var log = _mes.LogOn.FirstOrDefault(l => l.Id == id);
            if (log == null)
                return Json(new { success = false, message = "找不到該作業紀錄。" });

            // 確認作業是否已讀取
            if (!log.IsReaded)
                return Json(new { success = false, message = "此作業尚未讀取，無法結束。" });

            if (log.Status != LogOnStatus.LogOn && log.Status != LogOnStatus.Resume)
                return Json(new { success = false, message = "該紀錄不是進行中狀態，無法結束。" });

            log.EndTime = DateTime.Now;
            log.Status = LogOnStatus.LogOff;

            var duration = log.EndTime - log.StartTime; // 計算作業時間
            _mes.SaveChanges();

            return Json(new
            {
                success = true,
                message = "作業已結束",
                duration = duration?.TotalMinutes.ToString("F2") + " 分鐘"
            });
        }


        /// <summary>
        /// 取消作業 (Cancel)
        /// </summary>
        [HttpPost]
        public JsonResult CancelLogOn(int id)
        {
            var log = _mes.LogOn.FirstOrDefault(l => l.Id == id);
            if (log == null)
                return Json(new { success = false, message = "找不到該作業紀錄。" });

            log.Status = LogOnStatus.Cancel;
            log.EndTime = DateTime.Now;
            _mes.SaveChanges();

            return Json(new { success = true, message = "作業已取消" });
        }

        /// <summary>
        /// 讀取作業指示 (將 IsReaded 設為 true)
        /// 使用 ERP.User 資訊更新
        /// </summary>
        [HttpPost]
        public JsonResult ReadInstruction(int id, string userBarcode)
        {
            var log = _mes.LogOn.FirstOrDefault(l => l.Id == id);
            if (log == null)
                return Json(new { success = false, message = "找不到該作業紀錄。" });

            // 從 ERP.User 找出使用者
            var user = _erp.Users.FirstOrDefault(u => u.WorkId == userBarcode);
            if (user == null)
                return Json(new { success = false, message = "找不到對應的使用者。" });

            // 更新 LogOn 為已讀
            log.IsReaded = true;
            log.ReadTime = DateTime.Now;
            log.ReadUserName = user.Name;
            log.ReadUserWorkId = user.WorkId;

            _mes.SaveChanges();

            return Json(new
            {
                success = true,
                message = $"作業指示已讀取，由 {user.Name} ({user.WorkId}) 確認",
                readTime = log.ReadTime?.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

    }
}
