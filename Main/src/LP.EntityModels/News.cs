namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("ltl_News")]
    public partial class News
    {
        [Key]
        public int NewsID { get; set; }

        public DateTime Date { get; set; }

        public string BodyText { get; set; }

        public int Status { get; set; }

        [StringLength(400)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(30)]
        public string Culture { get; set; }

        public int? VisibilityOrder { get; set; }

        public int NewsCategoryID { get; set; }

        public virtual ltl_StatusBank ltl_StatusBank { get; set; }

        public virtual ltl_NewsCategory ltl_NewsCategory { get; set; }
    }
}
