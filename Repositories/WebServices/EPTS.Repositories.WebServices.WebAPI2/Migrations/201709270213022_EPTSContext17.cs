namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "StarTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TestResults", "FinishTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "FinishTime");
            DropColumn("dbo.TestResults", "StarTime");
        }
    }
}
