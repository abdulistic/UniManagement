namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderExtras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                        OrderDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.OrderDetail_Id)
                .Index(t => t.OrderDetail_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderExtras", "OrderDetail_Id", "dbo.OrderDetails");
            DropIndex("dbo.OrderExtras", new[] { "OrderDetail_Id" });
            DropTable("dbo.OrderExtras");
        }
    }
}
