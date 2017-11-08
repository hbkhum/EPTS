namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestGroups", "Sequence", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestGroups", "Sequence", c => c.String(nullable: false, maxLength: 3));
        }
    }
}
