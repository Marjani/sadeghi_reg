namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eddtilen12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "University", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(maxLength: 20));
            AlterColumn("dbo.RegisterForms", "University", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
