namespace WebApplication1.Migrations.MES
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMES : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogOns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserWorkId = c.String(),
                        WorkCenterId = c.Int(nullable: false),
                        WorkCenterName = c.String(),
                        ProcessId = c.Int(nullable: false),
                        ProcessCode = c.String(),
                        ProcessName = c.String(),
                        MachineId = c.Int(nullable: false),
                        MachineCode = c.String(),
                        NMachineNameCN = c.String(),
                        MachineNameEN = c.String(),
                        MachineCategory = c.String(),
                        WorkOrderBarcode = c.String(),
                        ProductionOrder = c.String(),
                        MaterialNumber = c.String(),
                        Route = c.String(),
                        ProductionStyle = c.String(),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        IsReaded = c.Boolean(nullable: false),
                        ReadTime = c.DateTime(),
                        ReadUserName = c.String(),
                        ReadUserWorkId = c.String(),
                        Status = c.String(),
                        Status1115 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        NameCN = c.String(),
                        NameEN = c.String(),
                        Status = c.String(),
                        Category = c.String(),
                        WorkCenterId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        MachineCategory = c.String(),
                        StandardTime = c.String(),
                        ReportMethod = c.String(),
                        WorkCenterId = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Processes");
            DropTable("dbo.Machines");
            DropTable("dbo.LogOns");
        }
    }
}
