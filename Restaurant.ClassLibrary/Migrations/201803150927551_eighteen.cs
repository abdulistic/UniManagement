namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eighteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderExtras", "Name", c => c.String());
            AddColumn("dbo.OrderExtras", "IsChecked", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderExtras", "ItemId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderExtras", "ItemId");
            DropColumn("dbo.OrderExtras", "IsChecked");
            DropColumn("dbo.OrderExtras", "Name");
        }
    }
}
