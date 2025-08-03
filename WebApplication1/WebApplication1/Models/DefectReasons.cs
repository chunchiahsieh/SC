using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DefectReasons
    {
        public int Id { get; set; }

        public int QCId { get; set; }

        public string Name { get; set; }

        public int Qty { get; set; }
    }
}