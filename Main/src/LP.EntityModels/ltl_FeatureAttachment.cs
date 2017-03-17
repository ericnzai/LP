namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_FeatureAttachment()
        {
            ltl_ClientAppFeatureAttachmentVisiblity = new HashSet<ltl_ClientAppFeatureAttachmentVisiblity>();
            ltl_FeatureAttachmentTranslation = new HashSet<ltl_FeatureAttachmentTranslation>();
            ltl_FeatureAttachment_CustomField = new HashSet<ltl_FeatureAttachment_CustomField>();
        }

        [Key]
        public int FeatureAttachmentID { get; set; }

        public int FeatureAttachmentTypeID { get; set; }

        public int? CSPostID { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int? Status { get; set; }

        public string Extra { get; set; }

        public string Parameters { get; set; }

        public int? CategoryID { get; set; }

        public int? SortOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_ClientAppFeatureAttachmentVisiblity> ltl_ClientAppFeatureAttachmentVisiblity { get; set; }

        public virtual ltl_FeatureAttachmentCategory ltl_FeatureAttachmentCategory { get; set; }

        public virtual ltl_FeatureAttachmentType ltl_FeatureAttachmentType { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachmentTranslation> ltl_FeatureAttachmentTranslation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_FeatureAttachment_CustomField> ltl_FeatureAttachment_CustomField { get; set; }
    }
}
