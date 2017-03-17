using LP.ServiceHost.DataContracts.Enums;

namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToTopicCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("exam.Answers", "StatusId", "exam.Statuses");
            DropForeignKey("exam.Attempts", "StatusId", "exam.Statuses");
            DropForeignKey("exam.Exams", "StatusId", "exam.Statuses");
            DropForeignKey("exam.ExamsQuestions", "StatusId", "exam.Statuses");
            DropForeignKey("exam.QuestionCategories", "StatusId", "exam.Statuses");
            DropForeignKey("exam.Questions", "StatusId", "exam.Statuses");
            DropForeignKey("exam.Responses", "StatusId", "exam.Statuses");
            DropIndex("exam.Answers", new[] { "StatusId" });
            DropIndex("exam.Questions", new[] { "StatusId" });
            DropIndex("exam.ExamsQuestions", new[] { "StatusId" });
            DropIndex("exam.Exams", new[] { "StatusId" });
            DropIndex("exam.Attempts", new[] { "StatusId" });
            DropIndex("exam.Responses", new[] { "StatusId" });
            DropIndex("exam.QuestionCategories", new[] { "StatusId" });
            AddColumn("exam.Answers", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.Questions", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.ExamsQuestions", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.Exams", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.Attempts", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.Responses", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("exam.QuestionCategories", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            AddColumn("dbo.ltl_TopicCategories", "Status", c => c.Int(nullable: false, defaultValue: (int)Status.Live));
            //DropTable("exam.Statuses");
        }
        
        public override void Down()
        {
            CreateTable(
                "exam.Statuses",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ltl_TopicCategories", "Status");
            DropColumn("exam.QuestionCategories", "Status");
            DropColumn("exam.Responses", "Status");
            DropColumn("exam.Attempts", "Status");
            DropColumn("exam.Exams", "Status");
            DropColumn("exam.ExamsQuestions", "Status");
            DropColumn("exam.Questions", "Status");
            DropColumn("exam.Answers", "Status");
            CreateIndex("exam.QuestionCategories", "StatusId");
            CreateIndex("exam.Responses", "StatusId");
            CreateIndex("exam.Attempts", "StatusId");
            CreateIndex("exam.Exams", "StatusId");
            CreateIndex("exam.ExamsQuestions", "StatusId");
            CreateIndex("exam.Questions", "StatusId");
            CreateIndex("exam.Answers", "StatusId");
            AddForeignKey("exam.Responses", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.Questions", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.QuestionCategories", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.ExamsQuestions", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.Exams", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.Attempts", "StatusId", "exam.Statuses", "Id");
            AddForeignKey("exam.Answers", "StatusId", "exam.Statuses", "Id");
        }
    }
}
