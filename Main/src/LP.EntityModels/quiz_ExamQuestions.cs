namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_ExamQuestions
    {
        [Key]
        public int ExamQuestionsID { get; set; }

        public int? ItemDescriptionID { get; set; }

        public int? StatusID { get; set; }

        public DateTime? RowVersion { get; set; }

        public int? QuestionID { get; set; }

        public int ExamID { get; set; }

        public virtual ltl_ItemDescription ltl_ItemDescription { get; set; }

        public virtual quiz_ExamBank quiz_ExamBank { get; set; }

        public virtual quiz_QuestionBank quiz_QuestionBank { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }
    }
}
