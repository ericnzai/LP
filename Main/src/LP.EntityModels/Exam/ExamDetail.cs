namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.ExamDetails")]
    public partial class ExamDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id { get; set; }

        public short? MaxNumberOfAttempts { get; set; }

        public byte MinPassMark { get; set; }

        public bool ShowQuestionAnswers { get; set; }

        public bool ShowHistoryControl { get; set; }

        public bool ShowQuestionResult { get; set; }

        public bool ShowQuestionScore { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
