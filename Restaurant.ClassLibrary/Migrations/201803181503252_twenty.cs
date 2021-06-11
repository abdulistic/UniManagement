namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twenty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.String());
            AddColumn("dbo.Orders", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.Orders", "Status");
        }
    }
}
