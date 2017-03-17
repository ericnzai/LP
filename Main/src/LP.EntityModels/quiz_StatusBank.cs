namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_StatusBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_StatusBank()
        {
            quiz_AnswerBank = new HashSet<quiz_AnswerBank>();
            quiz_Attempt = new HashSet<quiz_Attempt>();
            quiz_Category = new HashSet<quiz_Category>();
            quiz_CBMValue = new HashSet<quiz_CBMValue>();
            quiz_CommentBank = new HashSet<quiz_CommentBank>();
            quiz_ExamBank = new HashSet<quiz_ExamBank>();
            quiz_ExamQuestions = new HashSet<quiz_ExamQuestions>();
            quiz_QuestionBank = new HashSet<quiz_QuestionBank>();
            quiz_ResponseBank = new HashSet<quiz_ResponseBank>();
        }

        [Key]
        public int StatusBankID { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }

        public DateTime? RowVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_AnswerBank> quiz_AnswerBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_Attempt> quiz_Attempt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_Category> quiz_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_CBMValue> quiz_CBMValue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_CommentBank> quiz_CommentBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamBank> quiz_ExamBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamQuestions> quiz_ExamQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuestionBank> quiz_QuestionBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ResponseBank> quiz_ResponseBank { get; set; }
    }
}
