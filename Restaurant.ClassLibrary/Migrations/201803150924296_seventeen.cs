namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventeen : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderExtras", "Name");
            DropColumn("dbo.OrderExtras", "IsChecked");
            DropColumn("dbo.OrderExtras", "ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderExtras", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderExtras", "IsChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderExtras", "Name", c => c.String());
        }
    }
}
