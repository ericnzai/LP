namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.ResponsesAnswers")]
    public partial class ResponsesAnswer
    {
        public int Id { get; set; }

        public int ResponseId { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Response Respons { get; set; }
    }
}
