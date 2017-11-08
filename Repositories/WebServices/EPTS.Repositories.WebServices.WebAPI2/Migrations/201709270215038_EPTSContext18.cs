namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestResults", "TestUnitName", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.TestResults", "TestTypeName", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestResults", "TestTypeName");
            DropColumn("dbo.TestResults", "TestUnitName");
        }
    }
}
