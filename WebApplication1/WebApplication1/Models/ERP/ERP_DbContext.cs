﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models.ERP
{
    public class ERP_DbContext : DbContext
    {
        public ERP_DbContext() : base("name=ERP_DbContext") { }


        public DbSet<WorkCenters> WorkCenters { get; set; }


    }
}
