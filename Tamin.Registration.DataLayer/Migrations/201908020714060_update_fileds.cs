namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_fileds : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RegisterForms", new[] { "NatinalCode" });
            AlterColumn("dbo.RegisterForms", "FatherName", c => c.String(maxLength: 50));
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.RegisterForms", "ShenasnameCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.RegisterForms", "Telephone", c => c.String(maxLength: 15));
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForms", "Level", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.RegisterForms", "Telephone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.RegisterForms", "ShenasnameCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.RegisterForms", "NatinalCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.RegisterForms", "FatherName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.RegisterForms", "NatinalCode", unique: true);
        }
    }
}
