namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirtyThree : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChefOrderExtras", "ChefOrderDetail_Id", "dbo.ChefOrderDetails");
            DropForeignKey("dbo.ChefOrderDetails", "Item_Id", "dbo.Advertisements");
            DropForeignKey("dbo.ChefOrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ChefOrderDetails", "ChefOrder_Id", "dbo.ChefOrders");
            DropIndex("dbo.ChefOrderDetails", new[] { "Item_Id" });
            DropIndex("dbo.ChefOrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.ChefOrderDetails", new[] { "ChefOrder_Id" });
            DropIndex("dbo.ChefOrderExtras", new[] { "ChefOrderDetail_Id" });
            DropTable("dbo.ChefOrderDetails");
            DropTable("dbo.ChefOrderExtras");
            DropTable("dbo.ChefOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ChefOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedOn = c.DateTime(nullable: false),
                        User = c.String(),
                        Status = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ChefOrderExtras", "ChefOrderDetail_Id");
            CreateIndex("dbo.ChefOrderDetails", "ChefOrder_Id");
            CreateIndex("dbo.ChefOrderDetails", "Order_Id");
            CreateIndex("dbo.ChefOrderDetails", "Item_Id");
            AddForeignKey("dbo.ChefOrderDetails", "ChefOrder_Id", "dbo.ChefOrders", "Id");
            AddForeignKey("dbo.ChefOrderDetails", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.ChefOrderDetails", "Item_Id", "dbo.Advertisements", "Id");
            AddForeignKey("dbo.ChefOrderExtras", "ChefOrderDetail_Id", "dbo.ChefOrderDetails", "Id");
        }
    }
}
