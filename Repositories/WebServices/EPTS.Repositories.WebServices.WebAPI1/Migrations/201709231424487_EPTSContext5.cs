namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestGroupLinks", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestGroupLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestPlanLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestPlanLinks", "TestPlanId", "dbo.TestPlans");
            DropIndex("dbo.TestGroupLinks", new[] { "TestGroupId" });
            DropIndex("dbo.TestGroupLinks", new[] { "TestId" });
            DropIndex("dbo.TestPlanLinks", new[] { "TestPlanId" });
            DropIndex("dbo.TestPlanLinks", new[] { "TestGroupId" });
            AddColumn("dbo.Tests", "TestGroupId", c => c.Guid(nullable: false));
            AddColumn("dbo.TestGroups", "TestPlanId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Tests", "TestGroupId");
            CreateIndex("dbo.TestGroups", "TestPlanId");
            AddForeignKey("dbo.TestGroups", "TestPlanId", "dbo.TestPlans", "TestPlanId", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "TestGroupId", "dbo.TestGroups", "TestGroupId", cascadeDelete: true);
            DropTable("dbo.TestGroupLinks");
            DropTable("dbo.TestPlanLinks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestPlanLinks",
                c => new
                    {
                        TestPlanLinkId = c.Guid(nullable: false, identity: true),
                        TestPlanId = c.Guid(nullable: false),
                        TestGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestPlanLinkId);
            
            CreateTable(
                "dbo.TestGroupLinks",
                c => new
                    {
                        TestGroupLinkId = c.Guid(nullable: false, identity: true),
                        TestGroupId = c.Guid(nullable: false),
                        TestId = c.Guid(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestGroupLinkId);
            
            DropForeignKey("dbo.Tests", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestGroups", "TestPlanId", "dbo.TestPlans");
            DropIndex("dbo.TestGroups", new[] { "TestPlanId" });
            DropIndex("dbo.Tests", new[] { "TestGroupId" });
            DropColumn("dbo.TestGroups", "TestPlanId");
            DropColumn("dbo.Tests", "TestGroupId");
            CreateIndex("dbo.TestPlanLinks", "TestGroupId");
            CreateIndex("dbo.TestPlanLinks", "TestPlanId");
            CreateIndex("dbo.TestGroupLinks", "TestId");
            CreateIndex("dbo.TestGroupLinks", "TestGroupId");
            AddForeignKey("dbo.TestPlanLinks", "TestPlanId", "dbo.TestPlans", "TestPlanId", cascadeDelete: true);
            AddForeignKey("dbo.TestPlanLinks", "TestGroupId", "dbo.TestGroups", "TestGroupId", cascadeDelete: true);
            AddForeignKey("dbo.TestGroupLinks", "TestGroupId", "dbo.TestGroups", "TestGroupId", cascadeDelete: true);
            AddForeignKey("dbo.TestGroupLinks", "TestId", "dbo.Tests", "TestId", cascadeDelete: true);
        }
    }
}
