namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_PostTranslations
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }

        [StringLength(256)]
        public string Subject { get; set; }

        public string Body { get; set; }

        public string FormattedBOdy { get; set; }

        [StringLength(256)]
        public string PostName { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostTranslationID { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }
    }
}
