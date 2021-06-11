namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "SaturatedFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "TransFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Cholestrol", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Sodium", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Fiber", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Protein", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advertisements", "Protein");
            DropColumn("dbo.Advertisements", "Sugar");
            DropColumn("dbo.Advertisements", "Fiber");
            DropColumn("dbo.Advertisements", "Carbohydrates");
            DropColumn("dbo.Advertisements", "Sodium");
            DropColumn("dbo.Advertisements", "Cholestrol");
            DropColumn("dbo.Advertisements", "TransFat");
            DropColumn("dbo.Advertisements", "SaturatedFat");
            DropColumn("dbo.Advertisements", "Fat");
            DropColumn("dbo.Advertisements", "Calories");
        }
    }
}
