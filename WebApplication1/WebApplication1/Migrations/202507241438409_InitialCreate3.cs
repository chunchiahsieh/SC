namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processes", "MachineCategory", c => c.String());
            AlterColumn("dbo.Processes", "StandardTime", c => c.String());
            DropColumn("dbo.Processes", "MachineCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Processes", "MachineCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Processes", "StandardTime", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Processes", "MachineCategory");
        }
    }
}
