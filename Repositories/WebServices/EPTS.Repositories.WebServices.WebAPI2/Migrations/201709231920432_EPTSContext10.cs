namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext10 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TestGroups", new[] { "TestGroupName" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TestGroups", "TestGroupName", unique: true);
        }
    }
}
