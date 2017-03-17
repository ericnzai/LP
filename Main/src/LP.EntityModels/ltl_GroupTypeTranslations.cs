namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_GroupTypeTranslations
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string ImageName { get; set; }

        public virtual ltl_GroupType ltl_GroupType { get; set; }
    }
}
