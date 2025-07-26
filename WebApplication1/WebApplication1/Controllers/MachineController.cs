using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class MachineController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();
        // GET: Machine

        public ActionResult GetMachinesByWorkCenter(int? workCenterId)
        {
            var machines = _mes.Machines.AsQueryable();

            if (workCenterId.HasValue)
                machines = machines.Where(m => m.WorkCenterId == workCenterId.Value);

            return PartialView("_MachineTableBodyPartial", machines.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return PartialView();  // 預設會載入 Views/Machine/Create.cshtml 當 PartialView
        }

        [HttpPost]
        public ActionResult Create(Machine model)
        {
            if (ModelState.IsValid)
            {
                _mes.Machines.Add(model);
                _mes.SaveChanges();

                return Json(new { success = true });
            }
            return Json(new { success = false, message = "資料驗證失敗。" });
        }

        //  編輯 GET
        public ActionResult Edit(int id)
        {
            var machine = _mes.Machines.Find(id);
            if (machine == null) return HttpNotFound();

            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return PartialView(machine); 
        }
        // .編輯 POST
        [HttpPost]
        public ActionResult Edit(Machine model)
        {
            if (ModelState.IsValid)
            {
                var existing = _mes.Machines.Find(model.Id);
                if (existing == null)
                {
                    return Json(new { success = false, message = "找不到該機台資料。" });
                }
                existing.Code = model.Code;
                existing.NameCN = model.NameCN;
                existing.NameEN = model.NameEN;
                existing.Status = model.Status;
                existing.Category = model.Category;
                existing.WorkCenterId = model.WorkCenterId;
                existing.UpdatedAt = DateTime.Now;

                _mes.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "資料驗證失敗。" });
        }

        // 刪除 GET（確認畫面）
        public ActionResult Delete(int id)
        {
            var machine = _mes.Machines.Find(id);
            if (machine == null) return HttpNotFound();

            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return PartialView(machine);
        }
        //  刪除 POST
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var machine = _mes.Machines.Find(id);
            if (machine == null)
                return Json(new { success = false, message = "資料不存在" });

            _mes.Machines.Remove(machine);
            try
            {
                _mes.SaveChanges();
                return Json(new { success = true });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false, message = "資料已被修改或刪除" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}