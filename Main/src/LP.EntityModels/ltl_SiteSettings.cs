namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SiteSettings
    {
        [Key]
        public int SettingsID { get; set; }

        [StringLength(256)]
        public string SettingName { get; set; }

        public string SettingValue { get; set; }

        public int SettingTypeID { get; set; }

        public string PossibleValues { get; set; }

        public string Description { get; set; }

        public virtual ltl_SettingsType ltl_SettingsType { get; set; }
    }
}
