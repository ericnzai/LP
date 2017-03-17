namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFileSizeAndComputedDateCreatedFromVAConversionTool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ltl_VAConversionToolNotifications", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ltl_VAConversionTools", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ltl_VAConversionToolTranslations", "DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.ltl_VAConversionTools", "FileSize");
            DropColumn("dbo.ltl_VAConversionToolTranslations", "FileSize");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ltl_VAConversionToolTranslations", "FileSize", c => c.String());
            AddColumn("dbo.ltl_VAConversionTools", "FileSize", c => c.String());
            AlterColumn("dbo.ltl_VAConversionToolTranslations", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ltl_VAConversionTools", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ltl_VAConversionToolNotifications", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
