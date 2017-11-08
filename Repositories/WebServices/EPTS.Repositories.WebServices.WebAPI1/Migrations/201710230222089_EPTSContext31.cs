namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Materials", "AssemblyTypeId", "dbo.AssemblyTypes");
            DropIndex("dbo.AssemblyTypes", new[] { "Description" });
            DropIndex("dbo.Materials", new[] { "Description" });
            DropIndex("dbo.Materials", new[] { "AssemblyTypeId" });
            DropTable("dbo.AssemblyTypes");
            DropTable("dbo.Materials");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                        AssemblyTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            CreateTable(
                "dbo.AssemblyTypes",
                c => new
                    {
                        AssemblyTypeId = c.Guid(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AssemblyTypeId);
            
            CreateIndex("dbo.Materials", "AssemblyTypeId");
            CreateIndex("dbo.Materials", "Description", unique: true);
            CreateIndex("dbo.AssemblyTypes", "Description", unique: true);
            AddForeignKey("dbo.Materials", "AssemblyTypeId", "dbo.AssemblyTypes", "AssemblyTypeId", cascadeDelete: true);
        }
    }
}
