namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_field : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RegisterForms", new[] { "NatinalCode" });
            AlterColumn("dbo.RegisterForms", "Username", c => c.String(maxLength: 25));
            AlterColumn("dbo.RegisterForms", "Password", c => c.String(maxLength: 20));
            AlterColumn("dbo.RegisterForms", "NatinalCardPhoto", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterForms", "PostCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.RegisterForms", "City", c => c.String(maxLength: 20));
            AlterColumn("dbo.RegisterForms", "AltTelephone", c => c.String(maxLength: 15));
            AlterColumn("dbo.RegisterForms", "PhotoFilename", c => c.String(maxLength: 500));
            AlterColumn("dbo.RegisterForms", "University", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(nullable: true, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "Country", c => c.String(maxLength: 20));
            CreateIndex("dbo.RegisterForms", "NatinalCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.RegisterForms", new[] { "NatinalCode" });
            AlterColumn("dbo.RegisterForms", "Country", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RegisterForms", "Major", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterForms", "University", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "PhotoFilename", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.RegisterForms", "AltTelephone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.RegisterForms", "City", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(maxLength: 12));
            AlterColumn("dbo.RegisterForms", "PostCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "NatinalCardPhoto", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RegisterForms", "Password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RegisterForms", "Username", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.RegisterForms", "NatinalCode", unique: true);
        }
    }
}
