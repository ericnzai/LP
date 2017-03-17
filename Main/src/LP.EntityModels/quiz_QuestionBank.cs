namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_QuestionBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_QuestionBank()
        {
            quiz_AnswerBank = new HashSet<quiz_AnswerBank>();
            quiz_ExamQuestions = new HashSet<quiz_ExamQuestions>();
            quiz_QuestionBank1 = new HashSet<quiz_QuestionBank>();
            quiz_ResponseBank = new HashSet<quiz_ResponseBank>();
        }

        [Key]
        public int QuestionBankID { get; set; }

        public string Question { get; set; }

        public int StatusID { get; set; }

        public string Description { get; set; }

        public int QuestionTypeID { get; set; }

        public double? Weight { get; set; }

        public int? CreatedByUserID { get; set; }

        public int? LastUpdatedByUserID { get; set; }

        public DateTime? RowVersion { get; set; }

        public string Comment { get; set; }

        public int CategoryID { get; set; }

        public bool? IsLastQuestion { get; set; }

        [StringLength(50)]
        public string ImageName { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public int? OriginalQuestionID { get; set; }

        public int? ParentQuestionID { get; set; }

        public int? TranslatedFromID { get; set; }

        public DateTime? LastUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_AnswerBank> quiz_AnswerBank { get; set; }

        public virtual quiz_Category quiz_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamQuestions> quiz_ExamQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuestionBank> quiz_QuestionBank1 { get; set; }

        public virtual quiz_QuestionBank quiz_QuestionBank2 { get; set; }

        public virtual quiz_QuestionType quiz_QuestionType { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ResponseBank> quiz_ResponseBank { get; set; }
    }
}
