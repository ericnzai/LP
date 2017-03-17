namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissionNavigationPropertyToTrainingArea : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ltl_TrainingAreaPermissions", "TrainingAreaID");
            AddForeignKey("dbo.ltl_TrainingAreaPermissions", "TrainingAreaID", "dbo.ltl_TrainingArea", "TrainingAreaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ltl_TrainingAreaPermissions", "TrainingAreaID", "dbo.ltl_TrainingArea");
            DropIndex("dbo.ltl_TrainingAreaPermissions", new[] { "TrainingAreaID" });
        }
    }
}
