namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_OldIndication_EmploymentRole
    {
        [Key]
        public int OldIndicationEmploymentRoleID { get; set; }

        public int OldIndicationID { get; set; }

        public int EmploymentRoleID { get; set; }
    }
}
