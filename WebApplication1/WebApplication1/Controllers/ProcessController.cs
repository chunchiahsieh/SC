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
        private readonly MES_DbContext db = new MES_DbContext();
        // GET: Machine

        public ActionResult GetProcessByWorkCenter(int? workCenterId)
        {
            var machines = db.Processes.AsQueryable();

            if (workCenterId.HasValue)
                machines = machines.Where(m => m.WorkCenterId == workCenterId.Value);

            return PartialView("_MachineTableBodyPartial", machines.ToList());
        }

        // GET: Process/Create
        public ActionResult Create()
        {
            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Name).ToList();
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
                db.Processes.Add(process);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView(process);
        }

        // GET: Process/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = db.Processes.Find(id);
            if (process == null)
                return HttpNotFound();

            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Name).ToList();
            return PartialView(process);
        }

        // POST: Process/Edit/5
        [HttpPost]
        public ActionResult Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                var existing = db.Processes.Find(process.Id);
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

                db.SaveChanges();

                return Json(new { success = true });
            }
     
            return PartialView(process);
        }

        // GET: Process/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var process = db.Processes.FirstOrDefault(p => p.Id == id);
            if (process == null)
                return HttpNotFound();

            return PartialView(process);
        }

        // POST: Process/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var process = db.Processes.Find(id);
            if (process != null)
            {
                db.Processes.Remove(process);
                db.SaveChanges();
            }
            return Json(new { success = true });
        }
    }
}