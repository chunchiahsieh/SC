namespace WebApplication1.Migrations.ERP
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WorkCenters", newName: "Users");
            CreateTable(
                "dbo.WorkCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "WorkId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "WorkId");
            DropTable("dbo.WorkCenters");
            RenameTable(name: "dbo.Users", newName: "WorkCenters");
        }
    }
}
