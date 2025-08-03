using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class QCBasicInfo
    {
        public Models.ERP.User User { get; set; }
        public Models.ERP.WorkCenters WorkCenters { get; set; }

        public Models.Machine Machine { get; set; }

        public Models.Process Process { get; set; }

        public Models.ERP.WO Wo { get; set; }

        public Models.LogOn LogOn { get; set; }
    }
}