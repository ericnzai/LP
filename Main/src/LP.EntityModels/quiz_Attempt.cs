namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_Attempt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_Attempt()
        {
            quiz_ResponseBank = new HashSet<quiz_ResponseBank>();
        }

        [Key]
        public int AttemptID { get; set; }

        public int UserID { get; set; }

        public int ExamID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int StatusID { get; set; }

        public bool? AttemptPassed { get; set; }

        public bool DataExported { get; set; }

        public virtual quiz_ExamBank quiz_ExamBank { get; set; }

        public virtual quiz_StatusBank quiz_StatusBank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_ResponseBank> quiz_ResponseBank { get; set; }
    }
}
