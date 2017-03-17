namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_NavigationCulture
    {
        [Key]
        public int NavigationCultureID { get; set; }

        public int NavigationID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(16)]
        public string Culture { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int? UserID { get; set; }

        public virtual User askCore_Users { get; set; }

        public virtual ltl_Navigation ltl_Navigation { get; set; }
    }
}
