namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentyfive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Extras", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Extras", "Price");
        }
    }
}
