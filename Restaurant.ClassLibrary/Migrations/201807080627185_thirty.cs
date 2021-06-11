namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "City_Id", c => c.Int());
            CreateIndex("dbo.Users", "City_Id");
            AddForeignKey("dbo.Users", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropColumn("dbo.Users", "City_Id");
        }
    }
}
