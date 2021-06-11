namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 300, unicode: false),
                        Description = c.String(maxLength: 8000, unicode: false),
                        Price = c.Single(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        Status_Id = c.Int(),
                        IsFeatured = c.Boolean(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AdvertisementStatus", t => t.Status_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdvertisementImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300, unicode: false),
                        Caption = c.String(maxLength: 100, unicode: false),
                        Advertisement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id)
                .Index(t => t.Advertisement_Id);
            
            CreateTable(
                "dbo.AdvertisementStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Province_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.Province_Id, cascadeDelete: true)
                .Index(t => t.Province_Id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGenders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedOn = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginId = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                        ConfirmPassword = c.String(nullable: false, maxLength: 20, unicode: false),
                        ContactNumber = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        ImageUrl = c.String(maxLength: 255, unicode: false),
                        BirthDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        FullName = c.String(maxLength: 100, unicode: false),
                        Advertisement_Id = c.Int(),
                        Gender_Id = c.Int(),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Advertisement_Id)
                .ForeignKey("dbo.UserGenders", t => t.Gender_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Advertisement_Id)
                .Index(t => t.Gender_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StreetAddress = c.String(nullable: false, maxLength: 255, unicode: false),
                        City_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 300, unicode: false),
                        Caption = c.String(maxLength: 100, unicode: false),
                        Priority = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserImages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Gender_Id", "dbo.UserGenders");
            DropForeignKey("dbo.Users", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Users");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Province_Id", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Advertisements", "Status_Id", "dbo.AdvertisementStatus");
            DropForeignKey("dbo.AdvertisementImages", "Advertisement_Id", "dbo.Advertisements");
            DropForeignKey("dbo.Advertisements", "Category_Id", "dbo.Categories");
            DropIndex("dbo.UserImages", new[] { "User_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Gender_Id" });
            DropIndex("dbo.Users", new[] { "Advertisement_Id" });
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropIndex("dbo.Provinces", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Province_Id" });
            DropIndex("dbo.AdvertisementImages", new[] { "Advertisement_Id" });
            DropIndex("dbo.Advertisements", new[] { "Category_Id" });
            DropIndex("dbo.Advertisements", new[] { "Status_Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserImages");
            DropTable("dbo.Addresses");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.UserGenders");
            DropTable("dbo.Countries");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.AdvertisementStatus");
            DropTable("dbo.AdvertisementImages");
            DropTable("dbo.Categories");
            DropTable("dbo.Advertisements");
        }
    }
}
