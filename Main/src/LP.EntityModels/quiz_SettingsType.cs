namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_SettingsType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quiz_SettingsType()
        {
            quiz_QuizSettings = new HashSet<quiz_QuizSettings>();
        }

        [Key]
        public int SettingsTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SettingType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quiz_QuizSettings> quiz_QuizSettings { get; set; }
    }
}
