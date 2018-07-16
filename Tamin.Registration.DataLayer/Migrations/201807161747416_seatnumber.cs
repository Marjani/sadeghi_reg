namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seatnumber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        NatinalCode = c.String(nullable: false, maxLength: 10),
                        ShenasnameCode = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        City = c.String(nullable: false, maxLength: 20),
                        Adderess = c.String(nullable: false, maxLength: 1000),
                        Mobile = c.String(nullable: false, maxLength: 15),
                        Telephone = c.String(nullable: false, maxLength: 15),
                        AltTelephone = c.String(nullable: false, maxLength: 15),
                        PhotoFilename = c.String(nullable: false, maxLength: 500),
                        Degree = c.String(nullable: false, maxLength: 10),
                        Level = c.String(nullable: false, maxLength: 10),
                        Average = c.Double(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        SeatNumber = c.String(maxLength: 10),
                        RegisterOn = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        IsPaied = c.Boolean(nullable: false),
                        PaiedOn = c.DateTime(),
                        TraceNumber = c.String(),
                        ReferenceNumber = c.String(),
                        TransactionReferenceID = c.String(),
                        TransactionDate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.NatinalCode, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RegisterForms", new[] { "NatinalCode" });
            DropTable("dbo.RegisterForms");
        }
    }
}
