namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_QuestionOrder
    {
        [Key]
        public int QuestionOrderID { get; set; }

        public int? UserID { get; set; }

        public int? ExamID { get; set; }

        [StringLength(250)]
        public string QuestionOrder { get; set; }

        public virtual quiz_ExamBank quiz_ExamBank { get; set; }
    }
}
