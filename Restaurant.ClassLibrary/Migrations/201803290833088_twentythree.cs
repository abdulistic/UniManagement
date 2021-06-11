namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twentythree : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AdvertisementImages", name: "Advertisement_Id", newName: "FoodItem_Id");
            RenameIndex(table: "dbo.AdvertisementImages", name: "IX_Advertisement_Id", newName: "IX_FoodItem_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AdvertisementImages", name: "IX_FoodItem_Id", newName: "IX_Advertisement_Id");
            RenameColumn(table: "dbo.AdvertisementImages", name: "FoodItem_Id", newName: "Advertisement_Id");
        }
    }
}
