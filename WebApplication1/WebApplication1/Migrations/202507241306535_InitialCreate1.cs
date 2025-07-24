namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Machines", "WorkCenterId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Machines", "WorkCenterId");
        }
    }
}
