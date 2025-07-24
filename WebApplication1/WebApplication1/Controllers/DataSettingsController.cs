using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    
    public class DataSettingsController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();
        public ActionResult Index()
        {
            try
            {
                var workCenters = _erp.WorkCenters
                    .OrderBy(w => w.Name)
                    .ToList();

                var machines = _mes.Machines
                    .ToList();

                ViewBag.WorkCenters = workCenters;

                return View();
            }
            catch (Exception ex)
            {
                return Content("資料庫錯誤：" + ex.Message);
            }
        }

        
    }
}