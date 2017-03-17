namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_TranslationHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ltl_TranslationHistory()
        {
            ltl_TranslationHistoryData = new HashSet<ltl_TranslationHistoryData>();
        }

        [Key]
        public int TranslationHistoryID { get; set; }

        public DateTime DateTime { get; set; }

        public int UpdatedBy { get; set; }

        [Required]
        public string TranslationType { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ltl_TranslationHistoryData> ltl_TranslationHistoryData { get; set; }
    }
}
