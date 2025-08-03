using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QC
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


        //工單資訊
        public string WorkOrderBarcode { get; set; }
        public string ProductionOrder { get; set; }
        public string MaterialNumber { get; set; }
        public string Route { get; set; }
        public string ProductionStyle { get; set; }

        //LogOn資訊

        public int SamplingQty { get; set; }            // 抽檢數量
        public int DefectQty { get; set; }              // 不良數量

        public decimal? BoardThickness { get; set; }    // 板厚（單位 mm）
        public decimal? ViaCopper { get; set; }         // 孔銅（單位 μm）
        public decimal? SurfaceCopper { get; set; }     // 面銅（單位 μm）
        public DateTime? StartTime { get; set; }        // 檢測開始時間
        public string CurrentStatus { get; set; } = "待機中"; // 當前狀態（待機中、檢測中、完工等）
        public List<Models.DefectReasons> DefectReasons { get; set; } = new List<Models.DefectReasons>();

        public DateTime? EndTime { get; set; }

        //生產指示區
        public bool IsReadEngineeringInstruction { get; set; }        // 是否已讀工程指示
        public bool IsReadBusinessRequest { get; set; }               // 是否已讀業務需求
        public bool IsReadProductionNote { get; set; }                // 是否已讀生產指示

        public string Status { get; set; }


        public double ProductionTime
        {
            get
            {
                if (StartTime == null) return 0;

                var end = EndTime ?? DateTime.Now;
                return Math.Round((end - StartTime.Value).TotalMinutes, 2);
            }
        }
    }
}