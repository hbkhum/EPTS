namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationGroups",
                c => new
                    {
                        StationGroupId = c.Guid(nullable: false, identity: true),
                        StationId = c.Guid(nullable: false),
                        TestPlanId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StationGroupId)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: false)
                .ForeignKey("dbo.TestPlans", t => t.TestPlanId, cascadeDelete: false)
                .Index(t => t.StationId)
                .Index(t => t.TestPlanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StationGroups", "TestPlanId", "dbo.TestPlans");
            DropForeignKey("dbo.StationGroups", "StationId", "dbo.Stations");
            DropIndex("dbo.StationGroups", new[] { "TestPlanId" });
            DropIndex("dbo.StationGroups", new[] { "StationId" });
            DropTable("dbo.StationGroups");
        }
    }
}
