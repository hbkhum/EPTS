namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext21 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Stations", new[] { "StationName" });
            AlterColumn("dbo.Stations", "StationName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Stations", "StationName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stations", new[] { "StationName" });
            AlterColumn("dbo.Stations", "StationName", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("dbo.Stations", "StationName", unique: true);
        }
    }
}
