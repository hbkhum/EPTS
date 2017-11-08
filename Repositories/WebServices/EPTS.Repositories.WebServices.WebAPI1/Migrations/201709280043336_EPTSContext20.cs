namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestGroups", "TestGroupName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestGroups", "TestGroupName", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
