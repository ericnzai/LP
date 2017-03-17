namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_ExamBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_ExamBank()
        {
            quiz_Attempt = new HashSet<quiz_Attempt>();
            quiz_ExamBank1 = new HashSet<quiz_ExamBank>();
            quiz_ExamQuestions = new HashSet<quiz_ExamQuestions>();
            quiz_PostExams = new HashSet<quiz_PostExams>();
            quiz_QuestionOrder = new HashSet<quiz_QuestionOrder>();
        }

        [Key]
        public int ExamBankID { get; set; }

        public int? StatusID { get; set; }

        [StringLength(200)]
        public string ExamTitle { get; set; }

        public int? ItemDescriptionID { get; set; }

        public double? Weight { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? CreatedByUserID { get; set; }

        public int? LastUpdatedByUserID { get; set; }

        public DateTime? RowVersion { get; set; }

        public int? NumberOfAttemptsAllowed { get; set; }

        [StringLength(50)]
        public string Certificate { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public bool? CLAEnabled { get; set; }

        public int? MaxNumberOfAttempts { get; set; }

        public int? MinPassMark { get; set; }

        public bool? ShowQuestionAnswers { get; set; }

        public bool? ShowHistoryControl { get; set; }

        public bool? ShowQuestionResult { get; set; }

        public bool? ShowQuestionScore { get; set; }

        public bool? QuickCertification { get; set; }

        public int? TranslatedFromID { get; set; }

        public DateTime? LastUpdated { get; set; }

        public virtual ltl_ItemDescription ltl_ItemDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_Attempt> quiz_Attempt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamBank> quiz_ExamBank1 { get; set; }

        public virtual quiz_ExamBank quiz_ExamBank2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamQuestions> quiz_ExamQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_PostExams> quiz_PostExams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuestionOrder> quiz_QuestionOrder { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }
    }
}
