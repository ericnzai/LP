namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicTranslations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ltl_Topics", new[] { "TopicCategoryID" });
            CreateTable(
                "dbo.ltl_TopicCategoryTranslations",
                c => new
                    {
                        TopicCategoryId = c.Int(nullable: false),
                        Culture = c.String(nullable: false, maxLength: 13),
                        TopicCategoryTranslationId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        LastUpdatedByUserId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.TopicCategoryId, t.Culture })
                .ForeignKey("dbo.ltl_TopicCategories", t => t.TopicCategoryId, cascadeDelete: false)
                .ForeignKey("dbo.askCore_Users", t => t.LastUpdatedByUserId, cascadeDelete: false)
                .Index(t => t.TopicCategoryId)
                .Index(t => t.LastUpdatedByUserId);
            
            CreateTable(
                "dbo.ltl_TopicTranslations",
                c => new
                    {
                        TopicId = c.Int(nullable: false),
                        Culture = c.String(nullable: false, maxLength: 13),
                        TopicTranslationId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                        LastUpdatedByUserId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.TopicId, t.Culture })
                .ForeignKey("dbo.ltl_Topics", t => t.TopicId, cascadeDelete: false)
                .ForeignKey("dbo.askCore_Users", t => t.LastUpdatedByUserId, cascadeDelete: false)
                .Index(t => t.TopicId)
                .Index(t => t.LastUpdatedByUserId);
            
            AddColumn("dbo.ltl_Topics", "DateCreated", c => c.DateTime(nullable: false, defaultValue:DateTime.UtcNow));
            AddColumn("dbo.ltl_Topics", "CreatedByUserId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.ltl_TopicCategories", "DateCreated", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.ltl_TopicCategories", "CreatedByUserId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.ltl_Topics", "TopicCategoryId");
            CreateIndex("dbo.ltl_Topics", "CreatedByUserId");
            CreateIndex("dbo.ltl_TopicCategories", "CreatedByUserId");
            AddForeignKey("dbo.ltl_Topics", "CreatedByUserId", "dbo.askCore_Users", "UserID", cascadeDelete: false);
            AddForeignKey("dbo.ltl_TopicCategories", "CreatedByUserId", "dbo.askCore_Users", "UserID", cascadeDelete: false);
            DropColumn("dbo.ltl_Topics", "Name");
            DropColumn("dbo.ltl_Topics", "Culture");
            DropColumn("dbo.ltl_Topics", "LastUpdated");
            DropColumn("dbo.ltl_Topics", "LastUpdatedByUserID");
            DropColumn("dbo.ltl_TopicCategories", "Name");
            DropColumn("dbo.ltl_TopicCategories", "Culture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ltl_TopicCategories", "Culture", c => c.String(maxLength: 15));
            AddColumn("dbo.ltl_TopicCategories", "Name", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.ltl_Topics", "LastUpdatedByUserID", c => c.Int(nullable: false));
            AddColumn("dbo.ltl_Topics", "LastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ltl_Topics", "Culture", c => c.String(maxLength: 15));
            AddColumn("dbo.ltl_Topics", "Name", c => c.String(maxLength: 256));
            DropForeignKey("dbo.ltl_TopicTranslations", "LastUpdatedByUserId", "dbo.askCore_Users");
            DropForeignKey("dbo.ltl_TopicTranslations", "TopicId", "dbo.ltl_Topics");
            DropForeignKey("dbo.ltl_TopicCategoryTranslations", "LastUpdatedByUserId", "dbo.askCore_Users");
            DropForeignKey("dbo.ltl_TopicCategoryTranslations", "TopicCategoryId", "dbo.ltl_TopicCategories");
            DropForeignKey("dbo.ltl_TopicCategories", "CreatedByUserId", "dbo.askCore_Users");
            DropForeignKey("dbo.ltl_Topics", "CreatedByUserId", "dbo.askCore_Users");
            DropIndex("dbo.ltl_TopicTranslations", new[] { "LastUpdatedByUserId" });
            DropIndex("dbo.ltl_TopicTranslations", new[] { "TopicId" });
            DropIndex("dbo.ltl_TopicCategoryTranslations", new[] { "LastUpdatedByUserId" });
            DropIndex("dbo.ltl_TopicCategoryTranslations", new[] { "TopicCategoryId" });
            DropIndex("dbo.ltl_TopicCategories", new[] { "CreatedByUserId" });
            DropIndex("dbo.ltl_Topics", new[] { "CreatedByUserId" });
            DropIndex("dbo.ltl_Topics", new[] { "TopicCategoryId" });
            DropColumn("dbo.ltl_TopicCategories", "CreatedByUserId");
            DropColumn("dbo.ltl_TopicCategories", "DateCreated");
            DropColumn("dbo.ltl_Topics", "CreatedByUserId");
            DropColumn("dbo.ltl_Topics", "DateCreated");
            DropTable("dbo.ltl_TopicTranslations");
            DropTable("dbo.ltl_TopicCategoryTranslations");
            CreateIndex("dbo.ltl_Topics", "TopicCategoryID");
        }
    }
}
