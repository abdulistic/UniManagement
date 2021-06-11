namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eleven : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Name");
        }
    }
}
