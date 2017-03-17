namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachment_CustomFieldDefinition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_FeatureAttachment_CustomFieldDefinition()
        {
            ltl_FeatureAttachment_CustomField = new HashSet<ltl_FeatureAttachment_CustomField>();
        }

        [Key]
        public int CustomFieldDefinitionID { get; set; }

        [StringLength(300)]
        public string CustomFieldName { get; set; }

        public int? CustomFieldTypeID { get; set; }

        public int? FeatureAttachmentTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment_CustomField> ltl_FeatureAttachment_CustomField { get; set; }

        public virtual ltl_FeatureAttachment_CustomFieldType ltl_FeatureAttachment_CustomFieldType { get; set; }

        public virtual ltl_FeatureAttachmentType ltl_FeatureAttachmentType { get; set; }
    }
}
