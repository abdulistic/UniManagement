namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class six : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Extras_Id", "dbo.Extras");
            DropIndex("dbo.Categories", new[] { "Extras_Id" });
            AddColumn("dbo.Extras", "Category_Id", c => c.Int());
            CreateIndex("dbo.Extras", "Category_Id");
            AddForeignKey("dbo.Extras", "Category_Id", "dbo.Categories", "Id");
            DropColumn("dbo.Categories", "Extras_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Extras_Id", c => c.Int());
            DropForeignKey("dbo.Extras", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Extras", new[] { "Category_Id" });
            DropColumn("dbo.Extras", "Category_Id");
            CreateIndex("dbo.Categories", "Extras_Id");
            AddForeignKey("dbo.Categories", "Extras_Id", "dbo.Extras", "Id");
        }
    }
}
