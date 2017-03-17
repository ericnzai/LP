namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_DraftStatusType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_DraftStatusType()
        {
            ltl_PostTranslationDrafts = new HashSet<ltl_PostTranslationDrafts>();
            ltl_SectionTranslationDrafts = new HashSet<ltl_SectionTranslationDrafts>();
        }

        [Key]
        public int DraftStatusTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string DraftStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_PostTranslationDrafts> ltl_PostTranslationDrafts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslationDrafts> ltl_SectionTranslationDrafts { get; set; }
    }
}
