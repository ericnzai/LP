namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_MandatoryRolePerCulture
    {
        [Key]
        public int CultureRoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string CultureCode { get; set; }

        public int MandatoryRoleID { get; set; }

        public string Description { get; set; }
    }
}
