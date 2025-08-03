using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class QCViewModel
    {
        public QCBasicInfo BasicInfo { get; set; }

        public QCView QCView { get; set; }
    }
}