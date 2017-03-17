namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class askCore_UserDetails
    {
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string JobTitle { get; set; }

        [StringLength(250)]
        public string Institution { get; set; }

        [StringLength(300)]
        public string Address1 { get; set; }

        [StringLength(300)]
        public string Address2 { get; set; }

        [StringLength(300)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(50)]
        public string PostCode { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string WorkPhone { get; set; }

        [StringLength(50)]
        public string HomePhone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string FaxNumber { get; set; }

        public bool? DontAllowEmails { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public virtual User askCore_Users { get; set; }
    }
}
