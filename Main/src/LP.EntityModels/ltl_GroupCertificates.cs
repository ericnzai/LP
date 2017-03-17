namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_GroupCertificates
    {
        [Key]
        public int CertficateID { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        [StringLength(50)]
        public string Filename { get; set; }

        public bool? IsActive { get; set; }
    }
}
