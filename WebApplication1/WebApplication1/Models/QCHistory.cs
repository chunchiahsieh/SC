using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QCHistory
    {
        public int Id { get; set; }

        public int QCId { get; set; }                 // 對應原始 QC 主鍵
        public string ActionType { get; set; }        // Create / Update / Delete
        public string UserBarcode { get; set; }       // 操作人員工號
        public DateTime ActionTime { get; set; }      // 操作時間（建議預設 DateTime.Now）

        public string DataSnapshot { get; set; }      // 將 QC 物件序列化成 JSON 儲存
    }
}