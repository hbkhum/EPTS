namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BusinessUnits", new[] { "BusinessUnitName" });
            DropIndex("dbo.Families", new[] { "FamilyName" });
            DropIndex("dbo.Lines", new[] { "LineName" });
            DropIndex("dbo.Models", new[] { "ModelName" });
            DropIndex("dbo.PartNumbers", new[] { "PartNumberName" });
            DropIndex("dbo.Stations", new[] { "StationName" });
            DropIndex("dbo.StationTypes", new[] { "StationDescription" });
            AlterColumn("dbo.BusinessUnits", "BusinessUnitName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Families", "FamilyName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Lines", "LineName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Models", "ModelName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.PartNumbers", "PartNumberName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Stations", "StationName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.StationTypes", "StationDescription", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.BusinessUnits", "BusinessUnitName", unique: true);
            CreateIndex("dbo.Families", "FamilyName", unique: true);
            CreateIndex("dbo.Lines", "LineName", unique: true);
            CreateIndex("dbo.Models", "ModelName", unique: true);
            CreateIndex("dbo.PartNumbers", "PartNumberName", unique: true);
            CreateIndex("dbo.Stations", "StationName", unique: true);
            CreateIndex("dbo.StationTypes", "StationDescription", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.StationTypes", new[] { "StationDescription" });
            DropIndex("dbo.Stations", new[] { "StationName" });
            DropIndex("dbo.PartNumbers", new[] { "PartNumberName" });
            DropIndex("dbo.Models", new[] { "ModelName" });
            DropIndex("dbo.Lines", new[] { "LineName" });
            DropIndex("dbo.Families", new[] { "FamilyName" });
            DropIndex("dbo.BusinessUnits", new[] { "BusinessUnitName" });
            AlterColumn("dbo.StationTypes", "StationDescription", c => c.String(maxLength: 15));
            AlterColumn("dbo.Stations", "StationName", c => c.String(maxLength: 15));
            AlterColumn("dbo.PartNumbers", "PartNumberName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Models", "ModelName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Lines", "LineName", c => c.String(maxLength: 15));
            AlterColumn("dbo.Families", "FamilyName", c => c.String(maxLength: 30));
            AlterColumn("dbo.BusinessUnits", "BusinessUnitName", c => c.String(maxLength: 30));
            CreateIndex("dbo.StationTypes", "StationDescription", unique: true);
            CreateIndex("dbo.Stations", "StationName", unique: true);
            CreateIndex("dbo.PartNumbers", "PartNumberName", unique: true);
            CreateIndex("dbo.Models", "ModelName", unique: true);
            CreateIndex("dbo.Lines", "LineName", unique: true);
            CreateIndex("dbo.Families", "FamilyName", unique: true);
            CreateIndex("dbo.BusinessUnits", "BusinessUnitName", unique: true);
        }
    }
}
