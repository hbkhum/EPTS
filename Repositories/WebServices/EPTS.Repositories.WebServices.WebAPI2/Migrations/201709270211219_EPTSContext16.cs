namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultId = c.Guid(nullable: false, identity: true),
                        TestName = c.String(nullable: false, maxLength: 50),
                        Sequence = c.Int(nullable: false),
                        LoLimit = c.String(nullable: false, maxLength: 15),
                        TestUnitId = c.Guid(nullable: false),
                        HiLimit = c.String(nullable: false, maxLength: 15),
                        TestTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestResultId)
                .ForeignKey("dbo.TestTypes", t => t.TestTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TestUnits", t => t.TestUnitId, cascadeDelete: true)
                .Index(t => t.TestUnitId)
                .Index(t => t.TestTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "TestUnitId", "dbo.TestUnits");
            DropForeignKey("dbo.TestResults", "TestTypeId", "dbo.TestTypes");
            DropIndex("dbo.TestResults", new[] { "TestTypeId" });
            DropIndex("dbo.TestResults", new[] { "TestUnitId" });
            DropTable("dbo.TestResults");
        }
    }
}
