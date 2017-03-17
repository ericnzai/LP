namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_ItemDescription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_ItemDescription()
        {
            quiz_AnswerBank = new HashSet<quiz_AnswerBank>();
            quiz_Category = new HashSet<quiz_Category>();
            quiz_ExamBank = new HashSet<quiz_ExamBank>();
            quiz_ExamQuestions = new HashSet<quiz_ExamQuestions>();
            quiz_RolesBank = new HashSet<quiz_RolesBank>();
        }

        [Key]
        public int ItemDescriptionID { get; set; }

        [StringLength(50)]
        public string DescriptionName { get; set; }

        [StringLength(200)]
        public string Body { get; set; }

        public DateTime? RowVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_AnswerBank> quiz_AnswerBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_Category> quiz_Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamBank> quiz_ExamBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ExamQuestions> quiz_ExamQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_RolesBank> quiz_RolesBank { get; set; }
    }
}
