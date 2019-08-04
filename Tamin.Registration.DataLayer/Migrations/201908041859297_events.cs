namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class events : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RegisterForms", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.RegisterForms", "EventId");
            AddForeignKey("dbo.RegisterForms", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisterForms", "EventId", "dbo.Events");
            DropIndex("dbo.RegisterForms", new[] { "EventId" });
            DropColumn("dbo.RegisterForms", "EventId");
            DropTable("dbo.Events");
        }
    }
}
