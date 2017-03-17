namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_SiteSettings
    {
        [Key]
        public int SettingsID { get; set; }

        [Required]
        public string SettingName { get; set; }

        [Required]
        public string SettingValue { get; set; }

        public int SettingTypeID { get; set; }

        public string PossibleValues { get; set; }

        public virtual askCore_SettingsType askCore_SettingsType { get; set; }
    }
}
