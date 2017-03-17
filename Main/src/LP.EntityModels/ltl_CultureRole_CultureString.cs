namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_CultureRole_CultureString
    {
        [Key]
        public int CultureRoleCultureStringID { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(10)]
        public string CultureString { get; set; }
    }
}
