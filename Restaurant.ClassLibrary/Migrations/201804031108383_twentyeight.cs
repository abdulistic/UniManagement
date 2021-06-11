namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentyeight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "SubmittedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "SubmittedOn");
        }
    }
}
