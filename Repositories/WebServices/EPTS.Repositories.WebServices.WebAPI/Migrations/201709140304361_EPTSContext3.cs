namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tests", new[] { "TestName" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Tests", "TestName", unique: true);
        }
    }
}
