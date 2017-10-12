namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestGroups", "Sequence", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestGroups", "Sequence", c => c.String(maxLength: 3));
        }
    }
}
