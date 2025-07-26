using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ERP
{
    public class Instruction
    {
        public int Id { get; set; }

        public string MaterialNumber { get; set; }

        // 指示類型：工程、業務、生產
        public string InstructionType { get; set; } // "Engineering", "Business", "Production"

        // 指示標題
        public string Title { get; set; }

        // 指示內容
        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}