namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tests", new[] { "TestName" });
            AlterColumn("dbo.Tests", "TestName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Tests", "TestName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tests", new[] { "TestName" });
            AlterColumn("dbo.Tests", "TestName", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.Tests", "TestName", unique: true);
        }
    }
}
