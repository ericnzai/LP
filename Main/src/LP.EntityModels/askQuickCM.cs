namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("askQuickCMS")]
    public partial class askQuickCM
    {
        [Key]
        public int QuickCMSID { get; set; }

        [StringLength(50)]
        public string ContentName { get; set; }

        public string Content { get; set; }

        public int? Status { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        [StringLength(16)]
        public string Culture { get; set; }

        public int? UserID { get; set; }

        [StringLength(100)]
        public string PageType { get; set; }

        public bool IsCultureDependent { get; set; }

        public virtual User askCore_Users { get; set; }
    }
}
