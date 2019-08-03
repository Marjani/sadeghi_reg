namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_from_view : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterForms", "LastDegrePhotoFilename", c => c.String());
            AddColumn("dbo.RegisterForms", "University", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.RegisterForms", "Major", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.RegisterForms", "Country", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RegisterForms", "JobTelephone", c => c.String());
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.RegisterForms", "JobTelephone", c => c.Int(nullable: false));
            DropColumn("dbo.RegisterForms", "Country");
            DropColumn("dbo.RegisterForms", "Major");
            DropColumn("dbo.RegisterForms", "University");
            DropColumn("dbo.RegisterForms", "LastDegrePhotoFilename");
        }
    }
}
