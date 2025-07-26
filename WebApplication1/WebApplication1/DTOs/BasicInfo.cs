using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class BasicInfo
    {
        public Models.ERP.User User { get; set; }
        public Models.ERP.WorkCenters WorkCenters { get; set; }

        public Models.Machine Machine { get; set; }
        public List<Machine> Machines { get; set; }   // ←重點

        public Models.Process Process { get; set; }
    }
}