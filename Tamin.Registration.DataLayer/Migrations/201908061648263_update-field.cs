namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(maxLength: 10));
        }
    }
}
