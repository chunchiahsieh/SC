using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ERP
{
    public class WO
    {
        public int Id { get; set; }
        public string WorkOrderBarcode { get; set; } = string.Empty;
        public string ProductionOrder { get; set; } = string.Empty;
        public string MaterialNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string ProductionStyle { get; set; } = string.Empty;
        public int MaxQuantity { get; set; }
    }
}