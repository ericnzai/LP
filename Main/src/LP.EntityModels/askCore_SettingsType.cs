namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_SettingsType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public askCore_SettingsType()
        {
            askCore_SiteSettings = new HashSet<askCore_SiteSettings>();
        }

        [Key]
        public int SettingsTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SettingType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<askCore_SiteSettings> askCore_SiteSettings { get; set; }
    }
}
