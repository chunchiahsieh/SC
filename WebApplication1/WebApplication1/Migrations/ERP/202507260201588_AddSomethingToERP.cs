namespace WebApplication1.Migrations.ERP
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomethingToERP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialNumber = c.String(),
                        InstructionType = c.String(),
                        Title = c.String(),
                        Content = c.String(),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Instructions");
        }
    }
}
