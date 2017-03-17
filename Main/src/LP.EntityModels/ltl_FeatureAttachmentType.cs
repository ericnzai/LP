namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachmentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_FeatureAttachmentType()
        {
            ltl_FeatureAttachment = new HashSet<ltl_FeatureAttachment>();
            ltl_FeatureAttachment_CustomFieldDefinition = new HashSet<ltl_FeatureAttachment_CustomFieldDefinition>();
        }

        [Key]
        public int FeatureAttachmentTypeID { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public string Extra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment> ltl_FeatureAttachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment_CustomFieldDefinition> ltl_FeatureAttachment_CustomFieldDefinition { get; set; }
    }
}
