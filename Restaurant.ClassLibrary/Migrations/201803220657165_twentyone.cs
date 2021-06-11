namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentyone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChefOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        Order_Id = c.Int(),
                        ChefOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Item_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.ChefOrders", t => t.ChefOrder_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.ChefOrder_Id);
            
            CreateTable(
                "dbo.ChefOrderExtras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ChefOrderDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChefOrderDetails", t => t.ChefOrderDetail_Id)
                .Index(t => t.ChefOrderDetail_Id);
            
            CreateTable(
                "dbo.ChefOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedOn = c.DateTime(nullable: false),
                        Status = c.String(),
                        Price = c.Single(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChefOrders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ChefOrderDetails", "ChefOrder_Id", "dbo.ChefOrders");
            DropForeignKey("dbo.ChefOrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ChefOrderDetails", "Item_Id", "dbo.Advertisements");
            DropForeignKey("dbo.ChefOrderExtras", "ChefOrderDetail_Id", "dbo.ChefOrderDetails");
            DropIndex("dbo.ChefOrders", new[] { "User_Id" });
            DropIndex("dbo.ChefOrderExtras", new[] { "ChefOrderDetail_Id" });
            DropIndex("dbo.ChefOrderDetails", new[] { "ChefOrder_Id" });
            DropIndex("dbo.ChefOrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.ChefOrderDetails", new[] { "Item_Id" });
            DropTable("dbo.ChefOrders");
            DropTable("dbo.ChefOrderExtras");
            DropTable("dbo.ChefOrderDetails");
        }
    }
}
