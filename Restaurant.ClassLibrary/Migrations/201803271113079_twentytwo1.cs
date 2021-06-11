namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentytwo1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Users");
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            AddColumn("dbo.Users", "Address", c => c.String());
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StreetAddress = c.String(nullable: false, maxLength: 255, unicode: false),
                        City_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "Address");
            CreateIndex("dbo.Addresses", "City_Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Addresses", "City_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
