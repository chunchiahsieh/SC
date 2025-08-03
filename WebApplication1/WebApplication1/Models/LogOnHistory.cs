using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LogOnHistory
    {
        public int Id { get; set; }

        public int LogOnId { get; set; }             // 原始 LogOn 的主鍵
        public string ActionType { get; set; }       // Create / Update / Delete
        public string UserBarcode { get; set; }      // 操作人員工號
        public DateTime ActionTime { get; set; }     // 操作時間

        public string DataSnapshot { get; set; }     // JSON 快照
    }
}