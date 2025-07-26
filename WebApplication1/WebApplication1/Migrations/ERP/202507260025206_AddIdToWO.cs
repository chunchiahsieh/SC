namespace WebApplication1.Migrations.ERP
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdToWO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WOes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkOrderBarcode = c.String(),
                        ProductionOrder = c.String(),
                        MaterialNumber = c.String(),
                        Route = c.String(),
                        ProductionStyle = c.String(),
                        MaxQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WOes");
        }
    }
}
