namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ltl_Topics", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ltl_Topics", "Status");
        }
    }
}
