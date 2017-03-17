namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class quiz_RolesBank
    {
        [Key]
        public int RolesBankID { get; set; }

        [StringLength(50)]
        public string RoleTitle { get; set; }

        public DateTime? RowVersion { get; set; }

        public int? ItemDescriptionID { get; set; }

        public virtual ltl_ItemDescription ltl_ItemDescription { get; set; }
    }
}
