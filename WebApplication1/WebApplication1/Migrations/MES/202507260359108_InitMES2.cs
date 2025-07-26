namespace WebApplication1.Migrations.MES
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMES2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogOns", "StandardTime", c => c.Double(nullable: false));
            AlterColumn("dbo.Processes", "StandardTime", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Processes", "StandardTime", c => c.String());
            DropColumn("dbo.LogOns", "StandardTime");
        }
    }
}
