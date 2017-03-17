namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_DynamicRoles
    {
        [Key]
        public int DynamicRoleID { get; set; }

        public int ParentRoleID { get; set; }

        public int? ChildRoleID { get; set; }

        public int? DynamicRoleGroupID { get; set; }
    }
}
