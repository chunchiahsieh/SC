using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Enum
{
    public static class QCStatus
    {
        public const string LogOn = "LogOn";       // 登入
        public const string LogOff = "LogOff";     // 登出
        public const string Pause = "Pause";       // 暫停
        public const string Resume = "Resume";     // 恢復
        public const string Cancel = "Cancel";     // 取消

        public static readonly Dictionary<string, string> DisplayMap = new Dictionary<string, string>()
    {
        { LogOn, "檢測中" },
        { Pause, "已暫停" },
        { Cancel, "已取消" },
        { LogOff, "已完工" }
    };

    }
}