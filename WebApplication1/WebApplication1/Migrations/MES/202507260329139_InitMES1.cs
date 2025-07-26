namespace WebApplication1.Migrations.MES
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMES1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogOns", "OpUserName", c => c.String());
            AddColumn("dbo.LogOns", "OpUserWorkId", c => c.String());
            DropColumn("dbo.LogOns", "Status1115");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogOns", "Status1115", c => c.String());
            DropColumn("dbo.LogOns", "OpUserWorkId");
            DropColumn("dbo.LogOns", "OpUserName");
        }
    }
}
