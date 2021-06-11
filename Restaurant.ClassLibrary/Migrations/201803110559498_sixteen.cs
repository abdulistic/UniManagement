namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderExtras", "ItemId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderExtras", "ItemId");
        }
    }
}
