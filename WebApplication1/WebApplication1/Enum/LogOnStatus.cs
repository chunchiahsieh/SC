using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Enum
{
    public static class LogOnStatus
    {
        public const string LogOn = "LogOn";       // 登入
        public const string LogOff = "LogOff";     // 登出
        public const string Pause = "Pause";       // 暫停
        public const string Resume = "Resume";     // 恢復
        public const string Cancel = "Cancel";     // 取消
    }
}