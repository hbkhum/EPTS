namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext28 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stations", new[] { "StationCode" });
            AddColumn("dbo.StationGroups", "StationCode", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.StationGroups", "StationCode", unique: true);
            DropColumn("dbo.Stations", "StationCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "StationCode", c => c.String(nullable: false, maxLength: 15));
            DropIndex("dbo.StationGroups", new[] { "StationCode" });
            DropColumn("dbo.StationGroups", "StationCode");
            CreateIndex("dbo.Stations", "StationCode", unique: true);
        }
    }
}
