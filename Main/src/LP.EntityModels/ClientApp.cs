namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("ltl_ClientApp")]
    public partial class ClientApp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientApp()
        {
            ltl_ClientAppFeatureAttachmentVisiblity = new HashSet<ltl_ClientAppFeatureAttachmentVisiblity>();
            ltl_ClientAppPostVisiblity = new HashSet<ltl_ClientAppPostVisiblity>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Platform { get; set; }

        public int? FeatureAttachmentDefaultVisibility { get; set; }

        public int? PostDefaultVisibility { get; set; }

        public int Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_ClientAppFeatureAttachmentVisiblity> ltl_ClientAppFeatureAttachmentVisiblity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_ClientAppPostVisiblity> ltl_ClientAppPostVisiblity { get; set; }
    }
}
