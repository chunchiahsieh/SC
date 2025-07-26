using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class ProcessController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();
        // GET: Machine

        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var workCenter = _erp.WorkCenters.FirstOrDefault(w => w.Id == id.Value);
                ViewBag.workCenter = workCenter;
                ViewBag.Processes = _mes.Processes
                       .Where(p => p.WorkCenterId == workCenter.Id)
                       .OrderBy(p => p.Id)
                       .ToList();
            }
            ViewBag.Machines = _mes.Machines.OrderBy(m => m.Id).ToList();
           
            // 你可以再查其他資料
            return View();
        }

        public ActionResult GetProcessByWorkCenter(int? workCenterId)
        {
            var machines = _mes.Processes.AsQueryable();

            if (workCenterId.HasValue)
                machines = machines.Where(m => m.WorkCenterId == workCenterId.Value);

            return PartialView("_MachineTableBodyPartial", machines.ToList());
        }

        // GET: Process/Create
        public ActionResult Create()
        {
            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return PartialView();
        }

        // POST: Process/Create
        [HttpPost]
        public ActionResult Create(Process process)
        {
            if (ModelState.IsValid)
            {
                process.CreatedAt = DateTime.Now;
                process.UpdatedAt = DateTime.Now;
                _mes.Processes.Add(process);
                _mes.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView(process);
        }

        // GET: Process/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = _mes.Processes.Find(id);
            if (process == null)
                return HttpNotFound();

            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return PartialView(process);
        }

        // POST: Process/Edit/5
        [HttpPost]
        public ActionResult Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                var existing = _mes.Processes.Find(process.Id);
                if (existing == null)
                {
                    return HttpNotFound(); // 或 return Json(new { success = false, message = "資料不存在" });
                }

                // 更新欄位
                existing.Code = process.Code;
                existing.Name = process.Name;
                existing.MachineCategory = process.MachineCategory;
                existing.StandardTime = process.StandardTime;
                existing.ReportMethod = process.ReportMethod;
                existing.UpdatedAt = DateTime.Now;

                _mes.SaveChanges();

                return Json(new { success = true });
            }
     
            return PartialView(process);
        }

        // GET: Process/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = _mes.Processes.FirstOrDefault(p => p.Id == id);
            if (process == null)
                return HttpNotFound();

            return PartialView(process);
        }

        // POST: Process/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var process = _mes.Processes.Find(id);
            if (process != null)
            {
                _mes.Processes.Remove(process);
                _mes.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}