namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestGroups", "Desciption", c => c.String(maxLength: 50));
            AddColumn("dbo.TestPlans", "Desciption", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestPlans", "Desciption");
            DropColumn("dbo.TestGroups", "Desciption");
        }
    }
}
