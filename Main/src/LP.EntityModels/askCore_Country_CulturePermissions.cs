namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_Country_CulturePermissions
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string CountryCode { get; set; }

        public int CultureRoleID { get; set; }

        public string RestrictedAccessUrl { get; set; }

        public virtual Role askCore_Roles { get; set; }
    }
}
