using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly ERP_DbContext _db = new ERP_DbContext();


        [HttpGet]
        public JsonResult GetUserNameByBarcode(string barcode)
        {
            var user = _db.Users.FirstOrDefault(u => u.WorkId == barcode);
            if (user != null)
            {
                return Json(new { success = true, name = user.Name }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}