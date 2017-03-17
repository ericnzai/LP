namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachment_CustomFieldType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_FeatureAttachment_CustomFieldType()
        {
            ltl_FeatureAttachment_CustomFieldDefinition = new HashSet<ltl_FeatureAttachment_CustomFieldDefinition>();
        }

        [Key]
        public int CustomFieldTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomFieldType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment_CustomFieldDefinition> ltl_FeatureAttachment_CustomFieldDefinition { get; set; }
    }
}
