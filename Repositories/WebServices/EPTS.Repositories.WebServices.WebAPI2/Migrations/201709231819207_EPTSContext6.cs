namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TestPlans", new[] { "TestPlanName" });
            AddColumn("dbo.TestGroups", "Sequence", c => c.Int(nullable: false));
            AlterColumn("dbo.TestPlans", "TestPlanName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.TestPlans", "TestPlanName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TestPlans", new[] { "TestPlanName" });
            AlterColumn("dbo.TestPlans", "TestPlanName", c => c.String(nullable: false, maxLength: 15));
            DropColumn("dbo.TestGroups", "Sequence");
            CreateIndex("dbo.TestPlans", "TestPlanName", unique: true);
        }
    }
}
