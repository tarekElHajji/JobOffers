namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Jobs", "UserId");
            AddForeignKey("dbo.Jobs", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "UserId" });
            DropColumn("dbo.Jobs", "UserId");
        }
    }
}
