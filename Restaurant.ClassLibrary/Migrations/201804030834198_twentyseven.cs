namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentyseven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderExtras", "ExtraId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderExtras", "ExtraId");
        }
    }
}
