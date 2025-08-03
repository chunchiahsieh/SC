using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTOs
{
    public class QCView
    {
        public bool IsReadEngineeringInstruction { get; set; }        // 是否已讀工程指示
        public bool IsReadBusinessRequest { get; set; }               // 是否已讀業務需求
        public bool IsReadProductionNote { get; set; }                // 是否已讀生產指示

        public int SamplingQty { get; set; }            // 抽檢數量
        public int DefectQty { get; set; }              // 不良數量
        
        public decimal? BoardThickness { get; set; }    // 板厚（單位 mm）
        public decimal? ViaCopper { get; set; }         // 孔銅（單位 μm）
        public decimal? SurfaceCopper { get; set; }     // 面銅（單位 μm）
        public DateTime? StartTime { get; set; }        // 檢測開始時間
        public string CurrentStatus { get; set; } = "待機中"; // 當前狀態（待機中、檢測中、完工等）
        public List<Models.DefectReasons> DefectReasons { get; set; } = new List<Models.DefectReasons>();
    }
}