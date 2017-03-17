namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_CBMValue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_CBMValue()
        {
            quiz_ResponseBank = new HashSet<quiz_ResponseBank>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CBMValueID { get; set; }

        public int? StatusID { get; set; }

        public DateTime RowVersion { get; set; }

        public int CBMValue { get; set; }

        public int CBMGrade { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ResponseBank> quiz_ResponseBank { get; set; }
    }
}
