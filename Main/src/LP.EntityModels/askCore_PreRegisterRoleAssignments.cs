namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_PreRegisterRoleAssignments
    {
        [Key]
        public int PreRegisterRoleAssignmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string SemiColonSeperatedRoleNames { get; set; }

        [StringLength(50)]
        public string Complete { get; set; }

        public string Comment { get; set; }
    }
}
