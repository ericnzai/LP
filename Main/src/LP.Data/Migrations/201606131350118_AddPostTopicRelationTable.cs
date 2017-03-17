namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostTopicRelationTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ltl_PostsTopics", newName: "PostTopics");
            DropIndex("dbo.PostTopics", new[] { "TopicID" });
            CreateIndex("dbo.PostTopics", "TopicId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PostTopics", new[] { "TopicId" });
            CreateIndex("dbo.PostTopics", "TopicID");
            RenameTable(name: "dbo.PostTopics", newName: "ltl_PostsTopics");
        }
    }
}
