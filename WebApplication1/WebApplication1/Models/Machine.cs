using System;

namespace WebApplication1.Models
{
    public class Machine
    {
        public int Id { get; set; }        // 機台編號
        public string Code { get; set; }        // 機台編號
        public string NameCN { get; set; }           // 機台名稱-中文
        public string NameEN { get; set; }           // 機台名稱-英文
        public string Status { get; set; }           // 使用狀態
        public string Category { get; set; }         // 機台類別
        public int? WorkCenterId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}