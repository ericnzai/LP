namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVAConversionToolNotifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ltl_VAConversionToolNotifications", "CreatedByUserId", "dbo.askCore_Users");
            DropForeignKey("dbo.ltl_VAConversionToolNotifications", "VAConversionToolId", "dbo.ltl_VAConversionTools");
            DropIndex("dbo.ltl_VAConversionToolNotifications", new[] { "CreatedByUserId" });
            DropIndex("dbo.ltl_VAConversionToolNotifications", new[] { "VAConversionToolId" });
            DropTable("dbo.ltl_VAConversionToolNotifications");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.VAConversionToolNotificationId);
            
            CreateIndex("dbo.ltl_VAConversionToolNotifications", "VAConversionToolId");
            CreateIndex("dbo.ltl_VAConversionToolNotifications", "CreatedByUserId");
            AddForeignKey("dbo.ltl_VAConversionToolNotifications", "VAConversionToolId", "dbo.ltl_VAConversionTools", "VAConversionToolId", cascadeDelete: true);
            AddForeignKey("dbo.ltl_VAConversionToolNotifications", "CreatedByUserId", "dbo.askCore_Users", "UserID", cascadeDelete: true);
        }
    }
}
