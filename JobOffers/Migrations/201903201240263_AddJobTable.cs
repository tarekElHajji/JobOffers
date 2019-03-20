namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        JobContent = c.String(nullable: false),
                        JobImage = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Categorie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categorie_Id)
                .Index(t => t.Categorie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Categorie_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Categorie_Id" });
            DropTable("dbo.Jobs");
        }
    }
}
