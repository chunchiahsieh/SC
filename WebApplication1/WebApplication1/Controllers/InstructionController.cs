using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.ERP;

namespace WebApplication1.Controllers
{
    public class InstructionController : Controller
    {
        private readonly ERP_DbContext _erp = new ERP_DbContext();
        private readonly MES_DbContext _mes = new MES_DbContext();

        // GET: Instruction
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 共用方法：依類型取得指示
        /// </summary>
        private JsonResult GetInstructionByType(string materialNumber, string type)
        {
            if (string.IsNullOrEmpty(materialNumber))
                return Json(new { success = false, message = "料號不得為空。" }, JsonRequestBehavior.AllowGet);

            var instructions = _erp.Instructions
                .Where(i => i.MaterialNumber == materialNumber && i.InstructionType == type)
                .Select(i => new
                {
                    i.Title,
                    i.Content
                })
                .ToList();

            if (!instructions.Any())
                return Json(new { success = false, message = $"找不到 {type} 指示。" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = true, data = instructions }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得指定料號的所有指示 (QC + LogOn + Production)
        /// </summary>
        [HttpGet]
        public JsonResult GetAllInstructions(string materialNumber)
        {
            if (string.IsNullOrEmpty(materialNumber))
                return Json(new { success = false, message = "料號不得為空。" }, JsonRequestBehavior.AllowGet);

            var qc = _erp.Instructions
                .Where(i => i.MaterialNumber == materialNumber && i.InstructionType == "QC")
                .Select(i => new { i.Title, i.Content })
                .ToList();

            var logOn = _erp.Instructions
                .Where(i => i.MaterialNumber == materialNumber && i.InstructionType == "LogOn")
                .Select(i => new { i.Title, i.Content })
                .ToList();

            var production = _erp.Instructions
                .Where(i => i.MaterialNumber == materialNumber && i.InstructionType == "Production")
                .Select(i => new { i.Title, i.Content })
                .ToList();

            if (!qc.Any() && !logOn.Any() && !production.Any())
                return Json(new { success = false, message = "找不到任何指示。" }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                success = true,
                QC = qc,
                LogOn = logOn,
                Production = production
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBusinessInstruction(string materialNumber)
            => GetInstructionByType(materialNumber, "Business");

        [HttpGet]
        public JsonResult GetEngineeringInstruction(string materialNumber)
            => GetInstructionByType(materialNumber, "Engineering");

        [HttpGet]
        public JsonResult GetProductionInstruction(string materialNumber)
            => GetInstructionByType(materialNumber, "Production");
    }
}
