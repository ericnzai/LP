namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTopicsAndTopicCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ltl_Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        TopicCategoryID = c.Int(nullable: false),
                        Culture = c.String(maxLength: 15),
                        LastUpdated = c.DateTime(nullable: false),
                        LastUpdatedByUserID = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicID)
                .ForeignKey("dbo.ltl_TopicCategories", t => t.TopicCategoryID, cascadeDelete: true)
                .Index(t => t.TopicCategoryID);
            
            CreateTable(
                "dbo.ltl_TopicCategories",
                c => new
                    {
                        TopicCategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TopicCategoryID);
            
            CreateTable(
                "dbo.ltl_PostsTopics",
                c => new
                    {
                        PostID = c.Int(nullable: false),
                        TopicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.TopicID })
                .ForeignKey("dbo.ltl_Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.ltl_Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.TopicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ltl_PostsTopics", "TopicID", "dbo.ltl_Topics");
            DropForeignKey("dbo.ltl_PostsTopics", "PostID", "dbo.ltl_Posts");
            DropForeignKey("dbo.ltl_Topics", "TopicCategoryID", "dbo.ltl_TopicCategories");
            DropIndex("dbo.ltl_PostsTopics", new[] { "TopicID" });
            DropIndex("dbo.ltl_PostsTopics", new[] { "PostID" });
            DropIndex("dbo.ltl_Topics", new[] { "TopicCategoryID" });
            DropTable("dbo.ltl_PostsTopics");
            DropTable("dbo.ltl_TopicCategories");
            DropTable("dbo.ltl_Topics");
        }
    }
}
