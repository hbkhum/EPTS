namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestGroups", "Description", c => c.String(maxLength: 50));
            DropColumn("dbo.TestGroups", "Desciption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestGroups", "Desciption", c => c.String(maxLength: 50));
            DropColumn("dbo.TestGroups", "Description");
        }
    }
}
