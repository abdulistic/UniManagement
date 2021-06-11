namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentytwo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChefOrders", "User_Id", "dbo.Users");
            DropIndex("dbo.ChefOrders", new[] { "User_Id" });
            AddColumn("dbo.ChefOrders", "User", c => c.String());
            DropColumn("dbo.ChefOrders", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChefOrders", "User_Id", c => c.Int());
            DropColumn("dbo.ChefOrders", "User");
            CreateIndex("dbo.ChefOrders", "User_Id");
            AddForeignKey("dbo.ChefOrders", "User_Id", "dbo.Users", "Id");
        }
    }
}
