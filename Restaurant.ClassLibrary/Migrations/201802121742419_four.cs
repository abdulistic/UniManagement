namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Advertisements", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Advertisements", new[] { "Order_Id" });
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advertisements", t => t.Item_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Order_Id);
            
            DropColumn("dbo.Advertisements", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "Order_Id", c => c.Int());
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Item_Id", "dbo.Advertisements");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Item_Id" });
            DropTable("dbo.OrderDetails");
            CreateIndex("dbo.Advertisements", "Order_Id");
            AddForeignKey("dbo.Advertisements", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
