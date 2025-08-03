using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTOs
{
    public class EditLogon
    {
        public int Id { get; set; }
        public Models.ERP.WO wo { get; set; }
        public Models.LogOn logon { get; set; }
    }
}