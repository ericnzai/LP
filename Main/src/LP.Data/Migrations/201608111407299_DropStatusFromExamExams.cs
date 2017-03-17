namespace LP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropStatusFromExamExams : DbMigration
    {
        public override void Up()
        {
            DropColumn("exam.Exams", "Status");
        }
        
        public override void Down()
        {
            AddColumn("exam.Exams", "Status", c => c.Int(nullable: false));
        }
    }
}
