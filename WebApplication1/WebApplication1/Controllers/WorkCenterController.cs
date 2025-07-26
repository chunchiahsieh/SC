using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class WorkCenterController : Controller
    {
        // GET: WorkCenter
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        public ActionResult Index()
        {
            ViewBag.WorkCenters = _erp.WorkCenters.OrderBy(w => w.Id).ToList();
            return View();
        }


    }
}