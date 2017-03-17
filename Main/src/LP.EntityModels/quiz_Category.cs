namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_Category()
        {
            quiz_Category1 = new HashSet<quiz_Category>();
            quiz_QuestionBank = new HashSet<quiz_QuestionBank>();
        }

        [Key]
        public int CategoryID { get; set; }

        public int StatusID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        public int? ParentID { get; set; }

        public int? ItemDescriptionID { get; set; }

        public DateTime RowVersion { get; set; }

        public virtual ltl_ItemDescription ltl_ItemDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_Category> quiz_Category1 { get; set; }

        public virtual quiz_Category quiz_Category2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuestionBank> quiz_QuestionBank { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }
    }
}
