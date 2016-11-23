namespace TateGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivePlaceCount = c.Int(nullable: false),
                        BirthYear = c.Int(nullable: false),
                        Date = c.String(),
                        fc = c.String(),
                        Gender = c.String(),
                        mda = c.String(),
                        StartLetter = c.String(),
                        TotalWorks = c.Int(nullable: false),
                        Url = c.String(),
                        Birth_Id = c.Int(),
                        Death_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlaceTimes", t => t.Birth_Id)
                .ForeignKey("dbo.PlaceTimes", t => t.Death_Id)
                .Index(t => t.Birth_Id)
                .Index(t => t.Death_Id);
            
            CreateTable(
                "dbo.PlaceTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place_Name = c.String(maxLength: 128),
                        Time_StartYear = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.Place_Name)
                .ForeignKey("dbo.Times", t => t.Time_StartYear)
                .Index(t => t.Place_Name)
                .Index(t => t.Time_StartYear);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        PlaceName = c.String(),
                        PlaceType = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Times",
                c => new
                    {
                        StartYear = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.StartYear);
            
            CreateTable(
                "dbo.Movements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Era_Id = c.Int(),
                        Artist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Eras", t => t.Era_Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Era_Id)
                .Index(t => t.Artist_Id);
            
            CreateTable(
                "dbo.Eras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movements", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Movements", "Era_Id", "dbo.Eras");
            DropForeignKey("dbo.Artists", "Death_Id", "dbo.PlaceTimes");
            DropForeignKey("dbo.Artists", "Birth_Id", "dbo.PlaceTimes");
            DropForeignKey("dbo.PlaceTimes", "Time_StartYear", "dbo.Times");
            DropForeignKey("dbo.PlaceTimes", "Place_Name", "dbo.Places");
            DropIndex("dbo.Movements", new[] { "Artist_Id" });
            DropIndex("dbo.Movements", new[] { "Era_Id" });
            DropIndex("dbo.PlaceTimes", new[] { "Time_StartYear" });
            DropIndex("dbo.PlaceTimes", new[] { "Place_Name" });
            DropIndex("dbo.Artists", new[] { "Death_Id" });
            DropIndex("dbo.Artists", new[] { "Birth_Id" });
            DropTable("dbo.Eras");
            DropTable("dbo.Movements");
            DropTable("dbo.Times");
            DropTable("dbo.Places");
            DropTable("dbo.PlaceTimes");
            DropTable("dbo.Artists");
        }
    }
}
