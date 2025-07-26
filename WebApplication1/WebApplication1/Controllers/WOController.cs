using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Models.ERP;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class WOController : Controller
    {
        private ERP_DbContext _erp = new ERP_DbContext();
        private MES_DbContext _mes = new MES_DbContext();

        [HttpGet]
        public JsonResult GetWOByBarcode(string barcode)
        {
            var wo = _erp.WOes.FirstOrDefault(w => w.WorkOrderBarcode == barcode);
            if (wo != null)
            {
                return Json(new
                {
                    wo.ProductionOrder,
                    wo.MaterialNumber,
                    wo.Route,
                    wo.MaxQuantity
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        
    }
}
