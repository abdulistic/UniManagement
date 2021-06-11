namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class five : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Extras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExtraName = c.String(),
                        ExtraNutritionInfo_Calories = c.Int(nullable: false),
                        ExtraNutritionInfo_Fat = c.Int(nullable: false),
                        ExtraNutritionInfo_SaturatedFat = c.Int(nullable: false),
                        ExtraNutritionInfo_TransFat = c.Int(nullable: false),
                        ExtraNutritionInfo_Cholestrol = c.Int(nullable: false),
                        ExtraNutritionInfo_Sodium = c.Int(nullable: false),
                        ExtraNutritionInfo_Carbohydrates = c.Int(nullable: false),
                        ExtraNutritionInfo_Fiber = c.Int(nullable: false),
                        ExtraNutritionInfo_Sugar = c.Int(nullable: false),
                        ExtraNutritionInfo_Protein = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Advertisements", "NutritionInfo_Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_SaturatedFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_TransFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Cholestrol", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Sodium", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Fiber", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "NutritionInfo_Protein", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "Extras_Id", c => c.Int());
            CreateIndex("dbo.Categories", "Extras_Id");
            AddForeignKey("dbo.Categories", "Extras_Id", "dbo.Extras", "Id");
            DropColumn("dbo.Advertisements", "Calories");
            DropColumn("dbo.Advertisements", "Fat");
            DropColumn("dbo.Advertisements", "SaturatedFat");
            DropColumn("dbo.Advertisements", "TransFat");
            DropColumn("dbo.Advertisements", "Cholestrol");
            DropColumn("dbo.Advertisements", "Sodium");
            DropColumn("dbo.Advertisements", "Carbohydrates");
            DropColumn("dbo.Advertisements", "Fiber");
            DropColumn("dbo.Advertisements", "Sugar");
            DropColumn("dbo.Advertisements", "Protein");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advertisements", "Protein", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Sugar", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Fiber", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Carbohydrates", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Sodium", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Cholestrol", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "TransFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "SaturatedFat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Advertisements", "Calories", c => c.Int(nullable: false));
            DropForeignKey("dbo.Categories", "Extras_Id", "dbo.Extras");
            DropIndex("dbo.Categories", new[] { "Extras_Id" });
            DropColumn("dbo.Categories", "Extras_Id");
            DropColumn("dbo.Advertisements", "NutritionInfo_Protein");
            DropColumn("dbo.Advertisements", "NutritionInfo_Sugar");
            DropColumn("dbo.Advertisements", "NutritionInfo_Fiber");
            DropColumn("dbo.Advertisements", "NutritionInfo_Carbohydrates");
            DropColumn("dbo.Advertisements", "NutritionInfo_Sodium");
            DropColumn("dbo.Advertisements", "NutritionInfo_Cholestrol");
            DropColumn("dbo.Advertisements", "NutritionInfo_TransFat");
            DropColumn("dbo.Advertisements", "NutritionInfo_SaturatedFat");
            DropColumn("dbo.Advertisements", "NutritionInfo_Fat");
            DropColumn("dbo.Advertisements", "NutritionInfo_Calories");
            DropTable("dbo.Extras");
        }
    }
}
