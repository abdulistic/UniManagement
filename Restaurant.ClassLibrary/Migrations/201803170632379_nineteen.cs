namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nineteen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advertisements", "Calories", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Fat", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "SaturatedFat", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "TransFat", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Cholestrol", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Sodium", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Carbohydrates", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Fiber", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Sugar", c => c.Single(nullable: false));
            AddColumn("dbo.Advertisements", "Protein", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Calories", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Fat", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "SaturatedFat", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "TransFat", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Cholestrol", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Sodium", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Carbohydrates", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Fiber", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Sugar", c => c.Single(nullable: false));
            AddColumn("dbo.Extras", "Protein", c => c.Single(nullable: false));
            DropColumn("dbo.Advertisements", "NutritionInfo_Calories");
            DropColumn("dbo.Advertisements", "NutritionInfo_Fat");
            DropColumn("dbo.Advertisements", "NutritionInfo_SaturatedFat");
            DropColumn("dbo.Advertisements", "NutritionInfo_TransFat");
            DropColumn("dbo.Advertisements", "NutritionInfo_Cholestrol");
            DropColumn("dbo.Advertisements", "NutritionInfo_Sodium");
            DropColumn("dbo.Advertisements", "NutritionInfo_Carbohydrates");
            DropColumn("dbo.Advertisements", "NutritionInfo_Fiber");
            DropColumn("dbo.Advertisements", "NutritionInfo_Sugar");
            DropColumn("dbo.Advertisements", "NutritionInfo_Protein");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Calories");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Fat");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_SaturatedFat");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_TransFat");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Cholestrol");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Sodium");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Carbohydrates");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Fiber");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Sugar");
            DropColumn("dbo.Extras", "ExtraNutritionInfo_Protein");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Protein", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Fiber", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Sodium", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Cholestrol", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_TransFat", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_SaturatedFat", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Extras", "ExtraNutritionInfo_Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Protein", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Fiber", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Sodium", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Cholestrol", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_TransFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_SaturatedFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Calories", c => c.Int(nullable: false));
            DropColumn("dbo.Extras", "Protein");
            DropColumn("dbo.Extras", "Sugar");
            DropColumn("dbo.Extras", "Fiber");
            DropColumn("dbo.Extras", "Carbohydrates");
            DropColumn("dbo.Extras", "Sodium");
            DropColumn("dbo.Extras", "Cholestrol");
            DropColumn("dbo.Extras", "TransFat");
            DropColumn("dbo.Extras", "SaturatedFat");
            DropColumn("dbo.Extras", "Fat");
            DropColumn("dbo.Extras", "Calories");
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
