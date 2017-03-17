namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_EmailTemplate
    {
        [Key]
        public int EmailTemplateID { get; set; }

        [StringLength(50)]
        public string EmailName { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }
    }
}
