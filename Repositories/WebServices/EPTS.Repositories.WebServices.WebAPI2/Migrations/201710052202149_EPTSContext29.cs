namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "Result", c => c.String(nullable: false));
            AddColumn("dbo.TestResults", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "Status");
            DropColumn("dbo.TestResults", "Result");
        }
    }
}
