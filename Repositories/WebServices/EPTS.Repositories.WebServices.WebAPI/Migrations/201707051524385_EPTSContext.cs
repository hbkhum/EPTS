namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext : DbMigration
    {
        public override void Up()
        {

            
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Guid(nullable: false, identity: true),
                        TestGroupId = c.Guid(nullable: false),
                        TestName = c.String(nullable: false, storeType: "ntext"),
                        TestDesciption = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.TestGroups", t => t.TestGroupId, cascadeDelete: true)
                .Index(t => t.TestGroupId);
            
            CreateTable(
                "dbo.TestGroups",
                c => new
                    {
                        TestGroupId = c.Guid(nullable: false, identity: true),
                        TestGroupName = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestGroupId);
            
            CreateTable(
                "dbo.TestGroupLinks",
                c => new
                    {
                        TestGroupLinkId = c.Guid(nullable: false, identity: true),
                        TestGroupId = c.Guid(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Description = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestGroupLinkId)
                .ForeignKey("dbo.TestGroups", t => t.TestGroupId, cascadeDelete: true)
                .Index(t => t.TestGroupId);
            
            CreateTable(
                "dbo.TestLinks",
                c => new
                    {
                        TestLinkId = c.Guid(nullable: false, identity: true),
                        TestId = c.Guid(nullable: false),
                        LoLimit = c.String(nullable: false, storeType: "ntext"),
                        HiLimit = c.String(nullable: false, storeType: "ntext"),
                        TestUnitId = c.Guid(nullable: false),
                        TestTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestLinkId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.TestTypes", t => t.TestTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TestUnits", t => t.TestUnitId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.TestUnitId)
                .Index(t => t.TestTypeId);
            
            CreateTable(
                "dbo.TestTypes",
                c => new
                    {
                        TestTypeId = c.Guid(nullable: false, identity: true),
                        TestTypeName = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestTypeId);
            
            CreateTable(
                "dbo.TestUnits",
                c => new
                    {
                        TestUnitId = c.Guid(nullable: false, identity: true),
                        TestUnitName = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestUnitId);
            
            CreateTable(
                "dbo.TestPlans",
                c => new
                    {
                        TestPlanId = c.Guid(nullable: false, identity: true),
                        TestPlanName = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.TestPlanId);
            
            CreateTable(
                "dbo.TestPlanLinks",
                c => new
                    {
                        TestPlanLinkId = c.Guid(nullable: false, identity: true),
                        TestGroupId = c.Guid(nullable: false),
                        TestPlanId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestPlanLinkId)
                .ForeignKey("dbo.TestGroups", t => t.TestGroupId, cascadeDelete: true)
                .ForeignKey("dbo.TestPlans", t => t.TestPlanId, cascadeDelete: true)
                .Index(t => t.TestGroupId)
                .Index(t => t.TestPlanId);
            

            

            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestPlanLinks", "TestPlanId", "dbo.TestPlans");
            DropForeignKey("dbo.TestPlanLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestLinks", "TestUnitId", "dbo.TestUnits");
            DropForeignKey("dbo.TestLinks", "TestTypeId", "dbo.TestTypes");
            DropForeignKey("dbo.TestLinks", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestGroupLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.Tests", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.Stations", "StationTypeId", "dbo.StationTypes");
            DropForeignKey("dbo.Stations", "LineId", "dbo.Lines");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ModelDetails", "PartNumberId", "dbo.PartNumbers");
            DropForeignKey("dbo.ModelDetails", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "FamilyId", "dbo.Families");
            DropForeignKey("dbo.Families", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TestPlanLinks", new[] { "TestPlanId" });
            DropIndex("dbo.TestPlanLinks", new[] { "TestGroupId" });
            DropIndex("dbo.TestLinks", new[] { "TestTypeId" });
            DropIndex("dbo.TestLinks", new[] { "TestUnitId" });
            DropIndex("dbo.TestLinks", new[] { "TestId" });
            DropIndex("dbo.TestGroupLinks", new[] { "TestGroupId" });
            DropIndex("dbo.Tests", new[] { "TestGroupId" });
            DropIndex("dbo.Stations", new[] { "StationTypeId" });
            DropIndex("dbo.Stations", new[] { "LineId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ModelDetails", new[] { "PartNumberId" });
            DropIndex("dbo.ModelDetails", new[] { "ModelId" });
            DropIndex("dbo.Models", new[] { "FamilyId" });
            DropIndex("dbo.Families", new[] { "BusinessUnitId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TestPlanLinks");
            DropTable("dbo.TestPlans");
            DropTable("dbo.TestUnits");
            DropTable("dbo.TestTypes");
            DropTable("dbo.TestLinks");
            DropTable("dbo.TestGroupLinks");
            DropTable("dbo.TestGroups");
            DropTable("dbo.Tests");
            DropTable("dbo.StationTypes");
            DropTable("dbo.Stations");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PartNumbers");
            DropTable("dbo.ModelDetails");
            DropTable("dbo.Models");
            DropTable("dbo.Lines");
            DropTable("dbo.Families");
            DropTable("dbo.BusinessUnits");
        }
    }
}
