namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_PostExams
    {
        [Key]
        public int PostExamID { get; set; }

        public int? ExamBankID { get; set; }

        public int? PostID { get; set; }

        public bool? Grouped { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }

        public virtual quiz_ExamBank quiz_ExamBank { get; set; }
    }
}
