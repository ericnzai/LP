namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.QuestionsOrder")]
    public partial class QuestionsOrder
    {
        public int Id { get; set; }

        public int AttemptId { get; set; }

        public int QuestionId { get; set; }

        public short Order { get; set; }

        public virtual Attempt Attempt { get; set; }

        public virtual Question Question { get; set; }
    }
}
