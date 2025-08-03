using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ERP;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class QCController : Controller
    {
        private ERP_DbContext _erp = new ERP_DbContext();
        private MES_DbContext _mes = new MES_DbContext();


        public ActionResult Index(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "WorkCenter");

            var process = _mes.Processes.FirstOrDefault(w => w.Id == id.Value);
            if (process == null) return RedirectToAction("Index", "WorkCenter");

            var workCenter = _erp.WorkCenters.FirstOrDefault(w => w.Id == process.WorkCenterId);
            if (workCenter == null) return RedirectToAction("Index", "WorkCenter");

            ViewBag.WorkCenterName = workCenter.Name;

            var machines = _mes.Machines
                .Where(x => x.WorkCenterId == workCenter.Id)
                .OrderBy(m => m.Id)
                .ToList();

            var basicInfo = new QCBasicInfo
            {
                User = null,
                WorkCenters = workCenter,
                Machine = null,
                Process = process,
                Wo = new WO(),
                LogOn = new LogOn(),
            };

            // QCView = null，交由前端依據行為建立
            var model = new QCViewModel
            {
                IndexId = id.Value,
                BasicInfo = basicInfo,
                QCView = null
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult GetQC(GetQCRequestDto dto)
        {
            // 驗證資料是否存在
            var process = _mes.Processes.FirstOrDefault(p => p.Id == dto.IndexId);
            if (process == null)
                return Json(new { success = false, message = "找不到對應製程。" });

            var WorkCenters = _erp.WorkCenters.FirstOrDefault(p => p.Id == process.WorkCenterId);
            if (WorkCenters == null)
                return Json(new { success = false, message = "找不到對應工作中心。" });

            var user = _erp.Users.FirstOrDefault(u => u.WorkId == dto.UserBarcode);
            if (user == null)
                return Json(new { success = false, message = "找不到使用者資料。" });

            var wo = _erp.WOes.FirstOrDefault(w => w.WorkOrderBarcode == dto.Barcode);
            if (wo == null)
                return Json(new { success = false, message = "找不到工單資料。" });


            // 2. 查詢是否已有相同條件的 QC 資料
            var qc = _mes.QC.FirstOrDefault(q =>
                q.UserWorkId == dto.UserBarcode &&
                q.WorkOrderBarcode == dto.Barcode &&
                q.ProcessId == process.Id);

            if (qc != null)
                return Json(new { success = true, qcId = qc.Id });

            // 3. 若無則新增
            qc = new QC();
            qc.UserWorkId = user.WorkId;
            qc.UserName = user.Name;
            qc.WorkCenterId = WorkCenters.Id;
            qc.WorkCenterName = WorkCenters.Name;

            qc.ProcessId = process.Id;
            qc.ProcessCode = process.Code;
            qc.ProcessName = process.Name;
            qc.WorkOrderBarcode = wo.WorkOrderBarcode;
            qc.ProductionOrder = wo.ProductionOrder;
            qc.MaterialNumber = wo.MaterialNumber;
            qc.Route = wo.Route;
            qc.ProductionStyle = wo.ProductionStyle;

            _mes.QC.Add(qc);
            _mes.SaveChanges();

            int newQcId = qc.Id;
            return Json(new { success = true, qcId = newQcId });
        }

        public ActionResult QCViewMain(int id)
        {
            var qc = _mes.QC
              //  .Include(q => q.DefectReasons) 
                .FirstOrDefault(q => q.Id == id);
            if (qc == null) return HttpNotFound();

            var process = _mes.Processes.FirstOrDefault(w => w.Id == qc.ProcessId);
            if (process == null) return RedirectToAction("Index", "WorkCenter");

            var workCenter = _erp.WorkCenters.FirstOrDefault(w => w.Id == process.WorkCenterId);
            if (workCenter == null) return RedirectToAction("Index", "WorkCenter");

            ViewBag.WorkCenterName = workCenter.Name;

            var machines = _mes.Machines
                .Where(x => x.WorkCenterId == workCenter.Id)
                .OrderBy(m => m.Id)
                .ToList();

            var basicInfo = new QCBasicInfo
            {
                User = null,
                WorkCenters = workCenter,
                Machine = null,
                Process = process,
                Wo = new WO(),
                LogOn = new LogOn(),
            };

            // QCView = null，交由前端依據行為建立
            var qcView = new QCView
            {
                Id = qc.Id,
                MaterialNumber = qc.MaterialNumber,
                IsReadEngineeringInstruction = qc.IsReadEngineeringInstruction,
                IsReadBusinessRequest = qc.IsReadBusinessRequest,
                IsReadProductionNote = qc.IsReadProductionNote,
                SamplingQty = qc.SamplingQty,
                DefectQty = qc.DefectQty,
                BoardThickness = qc.BoardThickness,
                ViaCopper = qc.ViaCopper,
                SurfaceCopper = qc.SurfaceCopper,
                StartTime = qc.StartTime,
                CurrentStatus = qc.Status,
                DefectReasons = qc.DefectReasons
            };
            return PartialView(qcView);
        }

        [HttpPost]
        public JsonResult EngineeringRead(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null)  return Json(new { success = false, message = "找不到資料。" });

            qc.IsReadEngineeringInstruction = true;
            _mes.SaveChanges();
            return Json(new { success = true, message = "已讀取。" });
        }

        [HttpPost]
        public JsonResult BusinessRead(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.IsReadBusinessRequest = true;
            _mes.SaveChanges();
            return Json(new { success = true, message = "已讀取。" });
        }

        [HttpPost]
        public JsonResult ProductionRead(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.IsReadProductionNote = true;
            _mes.SaveChanges();
            return Json(new { success = true, message = "已讀取。" });
        }

        [HttpPost]
        public JsonResult LogOn(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.Status = Enum.QCStatus.LogOn;
            _mes.SaveChanges();
            return Json(new { success = true, status = "檢測中" });
        }

        [HttpPost]
        public JsonResult Pause(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.Status = Enum.QCStatus.Pause;
            _mes.SaveChanges();
            return Json(new { success = true, status = "已暫停" });
        }

        [HttpPost]
        public JsonResult Cancel(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.Status = Enum.QCStatus.Cancel;
            _mes.SaveChanges();
            return Json(new { success = true, status = "已取消" });
        }

        [HttpPost]
        public JsonResult LogOff(int id)
        {
            var qc = _mes.QC.Find(id);
            if (qc == null) return Json(new { success = false, message = "找不到資料。" });

            qc.Status = Enum.QCStatus.LogOff;
            _mes.SaveChanges();
            return Json(new { success = true, status = "已完工" });
        }


    }
}
