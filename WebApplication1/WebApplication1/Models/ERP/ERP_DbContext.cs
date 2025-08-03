using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ERP
{
    public class ERP_DbContext : DbContext
    {
        public ERP_DbContext() : base("name=ERP_DbContext") { }

        public DbSet<WorkCenters> WorkCenters { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<WO> WOes { get; set; }
        public DbSet<Instruction> Instructions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除 EF 預設複數命名慣例
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<WorkCenters>()
                .ToTable("WorkCenters")
                .HasKey(h => h.Id);

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(l => l.Id);


            modelBuilder.Entity<WO>()
                .ToTable("WOes")
                .HasKey(m => m.Id); 

            modelBuilder.Entity<Instruction>()
                .ToTable("Instructions")
                .HasKey(p => p.Id); 

        }

    }
}
