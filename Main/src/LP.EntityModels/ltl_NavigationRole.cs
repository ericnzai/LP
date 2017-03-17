namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_NavigationRole
    {
        [Key]
        public int NavigationRoleID { get; set; }

        public int NavigationID { get; set; }

        public int? RoleID { get; set; }

        public virtual ltl_Navigation ltl_Navigation { get; set; }
    }
}
