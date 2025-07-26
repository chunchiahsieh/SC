using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LogOn
    {
        public int Id { get; set; }

        //使用者資訊
        public string UserName { get; set; }
        public string UserWorkId { get; set; }

        //工作中心
        public int WorkCenterId { get; set; }
        public string WorkCenterName { get; set; } 

        //工序資訊
        public int ProcessId { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public double StandardTime { get; set; }


        //機台資訊
        public int MachineId { get; set; }
        public string MachineCode { get; set; }
        public string NMachineNameCN { get; set; }           // 機台名稱-中文
        public string MachineNameEN { get; set; }
        public string MachineCategory { get; set; }
        

        //工單資訊
        public string WorkOrderBarcode { get; set; } 
        public string ProductionOrder { get; set; } 
        public string MaterialNumber { get; set; } 
        public string Route { get; set; } 
        public string ProductionStyle { get; set; } 

        //LogOn資訊
        public decimal Qty { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        //生產指示區
        public bool IsReaded { get; set; } = false;
        public DateTime? ReadTime { get; set; }
        public string ReadUserName { get; set; }
        public string ReadUserWorkId { get; set; }

        public string Status { get; set; }
         //操作者資訊
        public string OpUserName { get; set; }
        public string OpUserWorkId { get; set; }


        [NotMapped]
        public double ProductionTime
        {
            get
            {
                var end = EndTime ?? DateTime.Now;
                return Math.Round((end - StartTime).TotalMinutes, 2);
            }
        }
    }
}