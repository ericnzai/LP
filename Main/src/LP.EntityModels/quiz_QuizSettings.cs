namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_QuizSettings
    {
        [Key]
        public int SettingsID { get; set; }

        [Required]
        public string SettingName { get; set; }

        public string SettingValue { get; set; }

        public int SettingTypeID { get; set; }

        public string PossibleValues { get; set; }

        public string Description { get; set; }

        public virtual quiz_SettingsType quiz_SettingsType { get; set; }
    }
}
