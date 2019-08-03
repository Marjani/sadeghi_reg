namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_from_nemidonam : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
