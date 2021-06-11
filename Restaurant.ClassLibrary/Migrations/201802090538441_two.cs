namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedOn = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "User_Id");
            AddForeignKey("dbo.Orders", "User_Id", "dbo.Users", "Id");
        }
    }
}
