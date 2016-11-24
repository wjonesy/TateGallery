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
                "dbo.Artworks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        acno = c.String(),
                        AcquisitionYear = c.Int(nullable: false),
                        All_Artists = c.String(),
                        Classification = c.String(),
                        ContributorCount = c.Int(nullable: false),
                        CreditLine = c.String(),
                        DateRange_EndYear = c.String(),
                        DateRange_StartYear = c.String(),
                        DateRange_Text = c.String(),
                        DateText = c.String(),
                        Dept = c.String(),
                        Dimensions = c.String(),
                        ForeignTitle = c.String(),
                        GroupTitle = c.String(),
                        Height = c.String(),
                        Inscription = c.String(),
                        Medium = c.String(),
                        MovementCount = c.Int(nullable: false),
                        SubjectCount = c.Int(nullable: false),
                        ThumbnailCopyright = c.String(),
                        ThumbnailUrl = c.String(),
                        Title = c.String(),
                        Units = c.String(),
                        Url = c.String(),
                        Width = c.String(),
                        CatalougeGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CatalogueGroups", t => t.CatalougeGroup_Id)
                .Index(t => t.CatalougeGroup_Id);
            
            CreateTable(
                "dbo.CatalogueGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessionRanges = c.String(),
                        CompleteStatus = c.String(),
                        GroupType = c.Int(),
                        ShortTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.ArtworkArtists",
                c => new
                    {
                        Artwork_Id = c.Int(nullable: false),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artwork_Id, t.Artist_Id })
                .ForeignKey("dbo.Artworks", t => t.Artwork_Id, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_Id, cascadeDelete: true)
                .Index(t => t.Artwork_Id)
                .Index(t => t.Artist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movements", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.Movements", "Era_Id", "dbo.Eras");
            DropForeignKey("dbo.Artists", "Death_Id", "dbo.PlaceTimes");
            DropForeignKey("dbo.Artists", "Birth_Id", "dbo.PlaceTimes");
            DropForeignKey("dbo.PlaceTimes", "Time_StartYear", "dbo.Times");
            DropForeignKey("dbo.PlaceTimes", "Place_Name", "dbo.Places");
            DropForeignKey("dbo.ArtworkArtists", "Artist_Id", "dbo.Artists");
            DropForeignKey("dbo.ArtworkArtists", "Artwork_Id", "dbo.Artworks");
            DropForeignKey("dbo.Artworks", "CatalougeGroup_Id", "dbo.CatalogueGroups");
            DropIndex("dbo.ArtworkArtists", new[] { "Artist_Id" });
            DropIndex("dbo.ArtworkArtists", new[] { "Artwork_Id" });
            DropIndex("dbo.Movements", new[] { "Artist_Id" });
            DropIndex("dbo.Movements", new[] { "Era_Id" });
            DropIndex("dbo.PlaceTimes", new[] { "Time_StartYear" });
            DropIndex("dbo.PlaceTimes", new[] { "Place_Name" });
            DropIndex("dbo.Artworks", new[] { "CatalougeGroup_Id" });
            DropIndex("dbo.Artists", new[] { "Death_Id" });
            DropIndex("dbo.Artists", new[] { "Birth_Id" });
            DropTable("dbo.ArtworkArtists");
            DropTable("dbo.Eras");
            DropTable("dbo.Movements");
            DropTable("dbo.Times");
            DropTable("dbo.Places");
            DropTable("dbo.PlaceTimes");
            DropTable("dbo.CatalogueGroups");
            DropTable("dbo.Artworks");
            DropTable("dbo.Artists");
        }
    }
}
