using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MES_DbContext : DbContext
    {
        public MES_DbContext() : base("name=MES_DbContext") { }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<LogOn> LogOn { get; set; }

        public DbSet<LogOnHistory> LogOnHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除 EF 預設複數命名慣例
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //LogOnHistory Fluent API 設定
            modelBuilder.Entity<LogOnHistory>()
                .ToTable("LogOnHistories")
                .HasKey(h => h.Id);

            //LogOn Fluent API 設定
            modelBuilder.Entity<LogOn>()
                .ToTable("LogOns")
                .HasKey(l => l.Id);


            // Machine Fluent API 設定
            modelBuilder.Entity<Machine>()
                .ToTable("Machines")
                .HasKey(m => m.Id); // 假設 Machine 有 Id

            //  Process Fluent API 設定
            modelBuilder.Entity<Process>()
                .ToTable("Processes")
                .HasKey(p => p.Id); // 假設 Process 有 Id

        }

    }
}
