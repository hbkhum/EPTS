namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessUnits",
                c => new
                    {
                        BusinessUnitId = c.Guid(nullable: false, identity: true),
                        BusinessUnitName = c.String(maxLength: 30),
                        BusinessUnitDescription = c.String(maxLength: 50),
                        BusinessUnitImage = c.Binary(),
                    })
                .PrimaryKey(t => t.BusinessUnitId)
                .Index(t => t.BusinessUnitName, unique: true);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        FamilyId = c.Guid(nullable: false, identity: true),
                        FamilyName = c.String(maxLength: 30),
                        BusinessUnitId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FamilyId)
                .ForeignKey("dbo.BusinessUnits", t => t.BusinessUnitId, cascadeDelete: true)
                .Index(t => t.FamilyName, unique: true)
                .Index(t => t.BusinessUnitId);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineId = c.Guid(nullable: false, identity: true),
                        LineName = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.LineId)
                .Index(t => t.LineName, unique: true);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Guid(nullable: false, identity: true),
                        ModelName = c.String(maxLength: 30),
                        FamilyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.Families", t => t.FamilyId, cascadeDelete: true)
                .Index(t => t.ModelName, unique: true)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbo.ModelDetails",
                c => new
                    {
                        ModelDetailId = c.Guid(nullable: false, identity: true),
                        ModelId = c.Guid(nullable: false),
                        PartNumberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ModelDetailId)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.PartNumbers", t => t.PartNumberId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.PartNumberId);
            
            CreateTable(
                "dbo.PartNumbers",
                c => new
                    {
                        PartNumberId = c.Guid(nullable: false, identity: true),
                        PartNumberName = c.String(maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 30),
                        Revision = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PartNumberId)
                .Index(t => t.PartNumberName, unique: true);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationId = c.Guid(nullable: false, identity: true),
                        StationName = c.String(maxLength: 15),
                        LineId = c.Guid(nullable: false),
                        StationTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StationId)
                .ForeignKey("dbo.Lines", t => t.LineId, cascadeDelete: true)
                .ForeignKey("dbo.StationTypes", t => t.StationTypeId, cascadeDelete: true)
                .Index(t => t.StationName, unique: true)
                .Index(t => t.LineId)
                .Index(t => t.StationTypeId);
            
            CreateTable(
                "dbo.StationTypes",
                c => new
                    {
                        StationTypeId = c.Guid(nullable: false, identity: true),
                        StationDescription = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.StationTypeId)
                .Index(t => t.StationDescription, unique: true);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Guid(nullable: false, identity: true),
                        TestName = c.String(nullable: false, maxLength: 15),
                        TestDesciption = c.String(nullable: false, maxLength: 50),
                        LoLimit = c.String(nullable: false, maxLength: 15),
                        HiLimit = c.String(nullable: false, maxLength: 15),
                        TestUnitId = c.Guid(nullable: false),
                        TestTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.TestTypes", t => t.TestTypeId, cascadeDelete: true)
                .ForeignKey("dbo.TestUnits", t => t.TestUnitId, cascadeDelete: true)
                .Index(t => t.TestName, unique: true)
                .Index(t => t.TestUnitId)
                .Index(t => t.TestTypeId);
            
            CreateTable(
                "dbo.TestTypes",
                c => new
                    {
                        TestTypeId = c.Guid(nullable: false, identity: true),
                        TestTypeName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestTypeId)
                .Index(t => t.TestTypeName, unique: true);
            
            CreateTable(
                "dbo.TestUnits",
                c => new
                    {
                        TestUnitId = c.Guid(nullable: false, identity: true),
                        TestUnitName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestUnitId)
                .Index(t => t.TestUnitName, unique: true);
            
            CreateTable(
                "dbo.TestGroups",
                c => new
                    {
                        TestGroupId = c.Guid(nullable: false, identity: true),
                        TestGroupName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestGroupId)
                .Index(t => t.TestGroupName, unique: true);
            
            CreateTable(
                "dbo.TestGroupLinks",
                c => new
                    {
                        TestGroupLinkId = c.Guid(nullable: false, identity: true),
                        TestGroupId = c.Guid(nullable: false),
                        TestId = c.Guid(nullable: false),
                        Sequence = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestGroupLinkId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.TestGroups", t => t.TestGroupId, cascadeDelete: true)
                .Index(t => t.TestGroupId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.TestPlanLinks",
                c => new
                    {
                        TestPlanLinkId = c.Guid(nullable: false, identity: true),
                        TestPlanId = c.Guid(nullable: false),
                        TestGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TestPlanLinkId)
                .ForeignKey("dbo.TestGroups", t => t.TestGroupId, cascadeDelete: true)
                .ForeignKey("dbo.TestPlans", t => t.TestPlanId, cascadeDelete: true)
                .Index(t => t.TestPlanId)
                .Index(t => t.TestGroupId);
            
            CreateTable(
                "dbo.TestPlans",
                c => new
                    {
                        TestPlanId = c.Guid(nullable: false, identity: true),
                        TestPlanName = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.TestPlanId)
                .Index(t => t.TestPlanName, unique: true);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestPlanLinks", "TestPlanId", "dbo.TestPlans");
            DropForeignKey("dbo.TestPlanLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestGroupLinks", "TestGroupId", "dbo.TestGroups");
            DropForeignKey("dbo.TestGroupLinks", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "TestUnitId", "dbo.TestUnits");
            DropForeignKey("dbo.Tests", "TestTypeId", "dbo.TestTypes");
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
            DropIndex("dbo.TestPlans", new[] { "TestPlanName" });
            DropIndex("dbo.TestPlanLinks", new[] { "TestGroupId" });
            DropIndex("dbo.TestPlanLinks", new[] { "TestPlanId" });
            DropIndex("dbo.TestGroupLinks", new[] { "TestId" });
            DropIndex("dbo.TestGroupLinks", new[] { "TestGroupId" });
            DropIndex("dbo.TestGroups", new[] { "TestGroupName" });
            DropIndex("dbo.TestUnits", new[] { "TestUnitName" });
            DropIndex("dbo.TestTypes", new[] { "TestTypeName" });
            DropIndex("dbo.Tests", new[] { "TestTypeId" });
            DropIndex("dbo.Tests", new[] { "TestUnitId" });
            DropIndex("dbo.Tests", new[] { "TestName" });
            DropIndex("dbo.StationTypes", new[] { "StationDescription" });
            DropIndex("dbo.Stations", new[] { "StationTypeId" });
            DropIndex("dbo.Stations", new[] { "LineId" });
            DropIndex("dbo.Stations", new[] { "StationName" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PartNumbers", new[] { "PartNumberName" });
            DropIndex("dbo.ModelDetails", new[] { "PartNumberId" });
            DropIndex("dbo.ModelDetails", new[] { "ModelId" });
            DropIndex("dbo.Models", new[] { "FamilyId" });
            DropIndex("dbo.Models", new[] { "ModelName" });
            DropIndex("dbo.Lines", new[] { "LineName" });
            DropIndex("dbo.Families", new[] { "BusinessUnitId" });
            DropIndex("dbo.Families", new[] { "FamilyName" });
            DropIndex("dbo.BusinessUnits", new[] { "BusinessUnitName" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TestPlans");
            DropTable("dbo.TestPlanLinks");
            DropTable("dbo.TestGroupLinks");
            DropTable("dbo.TestGroups");
            DropTable("dbo.TestUnits");
            DropTable("dbo.TestTypes");
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
