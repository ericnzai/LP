namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDatabaseGeneratedDateTimeFromTopicTranslation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ltl_TopicCategoryTranslations", "LastUpdated", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AlterColumn("dbo.ltl_TopicTranslations", "LastUpdated", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ltl_TopicTranslations", "LastUpdated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ltl_TopicCategoryTranslations", "LastUpdated", c => c.DateTime(nullable: false));
        }
    }
}
