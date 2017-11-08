namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext23 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stations", new[] { "StationName" });
            AddColumn("dbo.Stations", "StationCode", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.Stations", "StationCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stations", new[] { "StationCode" });
            DropColumn("dbo.Stations", "StationCode");
            CreateIndex("dbo.Stations", "StationName", unique: true);
        }
    }
}
