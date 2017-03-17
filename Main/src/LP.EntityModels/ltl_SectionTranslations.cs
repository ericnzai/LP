namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SectionTranslations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_SectionTranslations()
        {
            ltl_SectionTranslationDrafts = new HashSet<ltl_SectionTranslationDrafts>();
            ltl_SectionTranslationMapping = new HashSet<ltl_SectionTranslationMapping>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string LearningObjectives { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionTranslationID { get; set; }

        public virtual ltl_Sections ltl_Sections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslationDrafts> ltl_SectionTranslationDrafts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_SectionTranslationMapping> ltl_SectionTranslationMapping { get; set; }
    }
}
