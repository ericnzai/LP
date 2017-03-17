namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_FieldDefinition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public askCore_FieldDefinition()
        {
            askCore_SubscriptionExtraInfo = new HashSet<askCore_SubscriptionExtraInfo>();
            askCore_UserExtraInfo = new HashSet<askCore_UserExtraInfo>();
        }

        [Key]
        public int FieldDefinitionID { get; set; }

        [Required]
        [StringLength(300)]
        public string FieldName { get; set; }

        public int FieldTypeID { get; set; }

        public virtual askCore_FieldType askCore_FieldType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_SubscriptionExtraInfo> askCore_SubscriptionExtraInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_UserExtraInfo> askCore_UserExtraInfo { get; set; }
    }
}
