namespace Restaurant.ClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eight : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Extras", name: "Category_Id", newName: "Categories_Id");
            RenameIndex(table: "dbo.Extras", name: "IX_Category_Id", newName: "IX_Categories_Id");
            AddColumn("dbo.Extras", "Name", c => c.String());
            DropColumn("dbo.Extras", "ExtraName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Extras", "ExtraName", c => c.String());
            DropColumn("dbo.Extras", "Name");
            RenameIndex(table: "dbo.Extras", name: "IX_Categories_Id", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Extras", name: "Categories_Id", newName: "Category_Id");
        }
    }
}
