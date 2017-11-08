namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext30 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssemblyTypes",
                c => new
                    {
                        AssemblyTypeId = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AssemblyTypeId)
                .Index(t => t.Description, unique: true);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                        AssemblyTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId)
                .ForeignKey("dbo.AssemblyTypes", t => t.AssemblyTypeId, cascadeDelete: true)
                .Index(t => t.Description, unique: true)
                .Index(t => t.AssemblyTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "AssemblyTypeId", "dbo.AssemblyTypes");
            DropIndex("dbo.Materials", new[] { "AssemblyTypeId" });
            DropIndex("dbo.Materials", new[] { "Description" });
            DropIndex("dbo.AssemblyTypes", new[] { "Description" });
            DropTable("dbo.Materials");
            DropTable("dbo.AssemblyTypes");
        }
    }
}
