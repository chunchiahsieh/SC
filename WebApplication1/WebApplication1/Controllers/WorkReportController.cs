using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class WorkReportController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();

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
            var basicInfo = new BasicInfo
            {
                User = null,              // user 物件，前面要查詢或組好
                WorkCenters = workCenter, // workCenter 物件
                Machine = null,        // machine 物件（可為 null 或第一台機台）
                Machines = machines,      // machines 為 List<Machine>
                Process = process         // process 物件
            };


            var model = new WorkReportViewModel
            {
                BasicInfo = basicInfo,
                Machines = machines
            };
            return View(model);
        }


    }
}