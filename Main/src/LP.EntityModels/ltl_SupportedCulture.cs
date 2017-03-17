namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SupportedCulture
    {
        [Key]
        public int CultureID { get; set; }

        [StringLength(12)]
        public string Culture { get; set; }

        public bool ShowVideo { get; set; }

        public bool ShowSignature { get; set; }

        public bool ShowJobBag { get; set; }

        public bool OnCountryChangeOnly { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
