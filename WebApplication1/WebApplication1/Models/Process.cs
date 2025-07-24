using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Process
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MachineCategory { get; set; }
        public string StandardTime { get; set; }
        public string ReportMethod { get; set; }
        public int? WorkCenterId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}