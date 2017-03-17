namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_QuestionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_QuestionType()
        {
            quiz_QuestionBank = new HashSet<quiz_QuestionBank>();
        }

        [Key]
        public int QuestionTypeID { get; set; }

        [StringLength(50)]
        public string QuestionTypeName { get; set; }

        public DateTime? RowVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuestionBank> quiz_QuestionBank { get; set; }
    }
}
