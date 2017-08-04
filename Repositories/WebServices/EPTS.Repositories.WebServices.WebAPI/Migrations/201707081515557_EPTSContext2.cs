namespace EPTS.Repositories.WebServices.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EPTSContext2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModelDetails", "ModelId", "dbo.Models");
            DropForeignKey("dbo.ModelDetails", "PartNumberId", "dbo.PartNumbers");
            DropIndex("dbo.ModelDetails", new[] { "ModelId" });
            DropIndex("dbo.ModelDetails", new[] { "PartNumberId" });
            CreateTable(
                "dbo.ModelDetailModels",
                c => new
                    {
                        ModelDetail_ModelDetailId = c.Guid(nullable: false),
                        Model_ModelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModelDetail_ModelDetailId, t.Model_ModelId })
                .ForeignKey("dbo.ModelDetails", t => t.ModelDetail_ModelDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.Model_ModelId, cascadeDelete: true)
                .Index(t => t.ModelDetail_ModelDetailId)
                .Index(t => t.Model_ModelId);
            
            CreateTable(
                "dbo.PartNumberModelDetails",
                c => new
                    {
                        PartNumber_PartNumberId = c.Guid(nullable: false),
                        ModelDetail_ModelDetailId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PartNumber_PartNumberId, t.ModelDetail_ModelDetailId })
                .ForeignKey("dbo.PartNumbers", t => t.PartNumber_PartNumberId, cascadeDelete: true)
                .ForeignKey("dbo.ModelDetails", t => t.ModelDetail_ModelDetailId, cascadeDelete: true)
                .Index(t => t.PartNumber_PartNumberId)
                .Index(t => t.ModelDetail_ModelDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartNumberModelDetails", "ModelDetail_ModelDetailId", "dbo.ModelDetails");
            DropForeignKey("dbo.PartNumberModelDetails", "PartNumber_PartNumberId", "dbo.PartNumbers");
            DropForeignKey("dbo.ModelDetailModels", "Model_ModelId", "dbo.Models");
            DropForeignKey("dbo.ModelDetailModels", "ModelDetail_ModelDetailId", "dbo.ModelDetails");
            DropIndex("dbo.PartNumberModelDetails", new[] { "ModelDetail_ModelDetailId" });
            DropIndex("dbo.PartNumberModelDetails", new[] { "PartNumber_PartNumberId" });
            DropIndex("dbo.ModelDetailModels", new[] { "Model_ModelId" });
            DropIndex("dbo.ModelDetailModels", new[] { "ModelDetail_ModelDetailId" });
            DropTable("dbo.PartNumberModelDetails");
            DropTable("dbo.ModelDetailModels");
            CreateIndex("dbo.ModelDetails", "PartNumberId");
            CreateIndex("dbo.ModelDetails", "ModelId");
            AddForeignKey("dbo.ModelDetails", "PartNumberId", "dbo.PartNumbers", "PartNumberId", cascadeDelete: true);
            AddForeignKey("dbo.ModelDetails", "ModelId", "dbo.Models", "ModelId", cascadeDelete: true);
        }
    }
}
