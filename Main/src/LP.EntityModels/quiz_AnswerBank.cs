namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_AnswerBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_AnswerBank()
        {
            quiz_AnswerBank1 = new HashSet<quiz_AnswerBank>();
        }

        [Key]
        public int AnswerBankID { get; set; }

        public int? StatusID { get; set; }

        public string Answer { get; set; }

        public int QuestionID { get; set; }

        public int? ItemDescriptionID { get; set; }

        public double? Weight { get; set; }

        public string Comment { get; set; }

        public DateTime? RowVersion { get; set; }

        public bool? IsCorrect { get; set; }

        public int? OriginalAnswerID { get; set; }

        public int? ParentAnswerID { get; set; }

        public int? TranslatedFromID { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int? LastUpdatedByUserID { get; set; }

        public virtual ltl_ItemDescription ltl_ItemDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_AnswerBank> quiz_AnswerBank1 { get; set; }

        public virtual quiz_AnswerBank quiz_AnswerBank2 { get; set; }

        public virtual quiz_QuestionBank quiz_QuestionBank { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }
    }
}
