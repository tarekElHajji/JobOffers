namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplyForJobTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobsId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JobsId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "JobsId", "dbo.Jobs");
            DropIndex("dbo.ApplyForJobs", new[] { "UserId" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobsId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
