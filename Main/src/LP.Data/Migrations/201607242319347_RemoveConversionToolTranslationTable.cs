namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveConversionToolTranslationTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ltl_VAConversionToolTranslations", "VAConversionToolId", "dbo.ltl_VAConversionTools");
            DropIndex("dbo.ltl_VAConversionToolTranslations", new[] { "VAConversionToolId" });
            AddColumn("dbo.ltl_VAConversionTools", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ltl_VAConversionTools", "Culture", c => c.String(maxLength: 13));
            DropColumn("dbo.ltl_VAConversionTools", "FileDownloadPath");
            DropTable("dbo.ltl_VAConversionToolTranslations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ltl_VAConversionToolTranslations",
                c => new
                    {
                        VAConversionToolTranslationId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileDownloadPath = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        VAConversionToolId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Culture = c.String(maxLength: 13),
                    })
                .PrimaryKey(t => t.VAConversionToolTranslationId);
            
            AddColumn("dbo.ltl_VAConversionTools", "FileDownloadPath", c => c.String());
            DropColumn("dbo.ltl_VAConversionTools", "Culture");
            DropColumn("dbo.ltl_VAConversionTools", "Status");
            CreateIndex("dbo.ltl_VAConversionToolTranslations", "VAConversionToolId");
            AddForeignKey("dbo.ltl_VAConversionToolTranslations", "VAConversionToolId", "dbo.ltl_VAConversionTools", "VAConversionToolId", cascadeDelete: true);
        }
    }
}
