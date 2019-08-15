namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unilnatinal : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RegisterForms", "NatinalCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.RegisterForms", new[] { "NatinalCode" });
        }
    }
}
