using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class WorkReportViewModel
    {
        public BasicInfo BasicInfo { get; set; }
        public Models.ERP.WO WO { get; set; } = new Models.ERP.WO();
        public List<Machine> Machines { get; set; }
        public List<LogOn> LogOns { get; set; } = new List<LogOn>();
    }
}