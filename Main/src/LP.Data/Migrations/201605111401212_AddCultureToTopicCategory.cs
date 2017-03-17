namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCultureToTopicCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ltl_TopicCategories", "Culture", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ltl_TopicCategories", "Culture");
        }
    }
}
