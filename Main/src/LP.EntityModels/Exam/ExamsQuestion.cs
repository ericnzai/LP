using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.ExamsQuestions")]
    public partial class ExamsQuestion
    {
        public int Id { get; set; }

        public byte StatusId { get; set; }

        public int QuestionId { get; set; }

        public short ExamId { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual Question Question { get; set; }

        public Status Status { get; set; }
    }
}
