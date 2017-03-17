namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_HoverOverSettings
    {
        [Key]
        public int HoverOverSettingsID { get; set; }

        [StringLength(50)]
        public string Setting { get; set; }

        public bool? Value { get; set; }
    }
}
