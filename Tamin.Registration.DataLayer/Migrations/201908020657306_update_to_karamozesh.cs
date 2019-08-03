namespace Tamin.Registration.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_to_karamozesh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        EName = c.String(nullable: false, maxLength: 50),
                        EFamily = c.String(nullable: false, maxLength: 50),
                        NatinalCardPhoto = c.String(nullable: false, maxLength: 50),
                        Job = c.String(maxLength: 50),
                        JobTelephone = c.Int(nullable: false),
                        PostCode = c.String(nullable: false, maxLength: 50),
                        Peygiri = c.String(),
                        PeygiriPhotoFilename = c.String(),
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
