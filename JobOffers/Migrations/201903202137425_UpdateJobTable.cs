namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJobTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Categorie_Id" });
            RenameColumn(table: "dbo.Jobs", name: "Categorie_Id", newName: "CategoriesId");
            AlterColumn("dbo.Jobs", "CategoriesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CategoriesId");
            AddForeignKey("dbo.Jobs", "CategoriesId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Jobs", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Jobs", "CategoriesId", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoriesId" });
            AlterColumn("dbo.Jobs", "CategoriesId", c => c.Int());
            RenameColumn(table: "dbo.Jobs", name: "CategoriesId", newName: "Categorie_Id");
            CreateIndex("dbo.Jobs", "Categorie_Id");
            AddForeignKey("dbo.Jobs", "Categorie_Id", "dbo.Categories", "Id");
        }
    }
}
