namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_FieldType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public askCore_FieldType()
        {
            askCore_FieldDefinition = new HashSet<askCore_FieldDefinition>();
        }

        [Key]
        public int FieldTypeID { get; set; }

        [StringLength(50)]
        public string FieldType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_FieldDefinition> askCore_FieldDefinition { get; set; }
    }
}
