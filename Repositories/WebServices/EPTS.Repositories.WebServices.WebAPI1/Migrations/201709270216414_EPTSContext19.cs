namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "TestId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TestResults", "TestId");
            AddForeignKey("dbo.TestResults", "TestId", "dbo.Tests", "TestId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TestId", "dbo.Tests");
            DropIndex("dbo.TestResults", new[] { "TestId" });
            DropColumn("dbo.TestResults", "TestId");
        }
    }
}
