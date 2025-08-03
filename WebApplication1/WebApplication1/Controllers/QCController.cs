using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ERP;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;

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



            // 組裝 BasicInfo DTO (舉例)
            var basicInfo = new QCBasicInfo
            {
                User = null,              // user 物件，前面要查詢或組好
                WorkCenters = workCenter, // workCenter 物件
                Machine = null,        // machine 物件（可為 null 或第一台機台）

                Process = process,         // process 物件
                Wo = new WO(),
                LogOn = new LogOn(),
            };

            var QCView = new QCView();
            QCView.IsReadEngineeringInstruction = true;
            QCView.IsReadBusinessRequest = true;
            QCView.IsReadProductionNote = true;

            QCView.CurrentStatus = Enum.LogOnStatus.LogOn;


            var model = new QCViewModel
            {
                BasicInfo = basicInfo,
                QCView = QCView,

            };
            return View(model);
        }


        [HttpPost]
        [Route("api/qc/{action}")]
        public JsonResult Start()
        {
            // TODO: 實際處理 QC 開始邏輯，例如更新狀態、時間、Log紀錄等
            return Json(new { success = true, status = "檢測中" });
        }

        [HttpPost]
        [Route("api/qc/pause")]
        public JsonResult Pause()
        {
            return Json(new { success = true, status = "已暫停" });
        }

        [HttpPost]
        [Route("api/qc/cancel")]
        public JsonResult Cancel()
        {
            return Json(new { success = true, status = "已取消" });
        }

        [HttpPost]
        [Route("api/qc/complete")]
        public JsonResult Complete()
        {
            return Json(new { success = true, status = "已完工" });
        }
    }
}
