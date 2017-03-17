namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_HoverOverAudioCulture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte hoac_Id { get; set; }

        [Required]
        [StringLength(16)]
        public string hoac_Culture { get; set; }

        public bool hoac_AudioEnabled { get; set; }

        public bool hoac_DispayEnglishText { get; set; }
    }
}
