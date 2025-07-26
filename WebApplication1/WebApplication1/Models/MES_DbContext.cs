using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class MES_DbContext : DbContext
    {
        public MES_DbContext() : base("name=MES_DbContext") { }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<LogOn> LogOn { get; set; }
        
    }
}
