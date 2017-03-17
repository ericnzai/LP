namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVAConversionTool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ltl_VAConversionToolNotifications",
                c => new
                    {
                        VAConversionToolNotificationId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.Int(nullable: false),
                        VAConversionToolId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VAConversionToolNotificationId)
                .ForeignKey("dbo.askCore_Users", t => t.CreatedByUserId, cascadeDelete: false)
                .ForeignKey("dbo.ltl_VAConversionTools", t => t.VAConversionToolId, cascadeDelete: true)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.VAConversionToolId);
            
            CreateTable(
                "dbo.ltl_VAConversionTools",
                c => new
                    {
                        VAConversionToolId = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        FileName = c.String(),
                        FileSize = c.String(),
                        FileDownloadPath = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.VAConversionToolId)
                .ForeignKey("dbo.askCore_Users", t => t.CreatedByUserId, cascadeDelete: false)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.ltl_VAConversionToolTranslations",
                c => new
                    {
                        VAConversionToolTranslationId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileSize = c.String(),
                        FileDownloadPath = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        VAConversionToolId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Culture = c.String(maxLength: 13),
                    })
                .PrimaryKey(t => t.VAConversionToolTranslationId)
                .ForeignKey("dbo.ltl_VAConversionTools", t => t.VAConversionToolId, cascadeDelete: true)
                .Index(t => t.VAConversionToolId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ltl_VAConversionToolTranslations", "VAConversionToolId", "dbo.ltl_VAConversionTools");
            DropForeignKey("dbo.ltl_VAConversionToolNotifications", "VAConversionToolId", "dbo.ltl_VAConversionTools");
            DropForeignKey("dbo.ltl_VAConversionTools", "CreatedByUserId", "dbo.askCore_Users");
            DropForeignKey("dbo.ltl_VAConversionToolNotifications", "CreatedByUserId", "dbo.askCore_Users");
            DropIndex("dbo.ltl_VAConversionToolTranslations", new[] { "VAConversionToolId" });
            DropIndex("dbo.ltl_VAConversionTools", new[] { "CreatedByUserId" });
            DropIndex("dbo.ltl_VAConversionToolNotifications", new[] { "VAConversionToolId" });
            DropIndex("dbo.ltl_VAConversionToolNotifications", new[] { "CreatedByUserId" });
            DropTable("dbo.ltl_VAConversionToolTranslations");
            DropTable("dbo.ltl_VAConversionTools");
            DropTable("dbo.ltl_VAConversionToolNotifications");
        }
    }
}
