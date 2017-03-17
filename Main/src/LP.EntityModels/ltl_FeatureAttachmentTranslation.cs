namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_FeatureAttachmentTranslation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeatureAttachmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(13)]
        public string Culture { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int? Status { get; set; }

        public string Extra { get; set; }

        public string Parameters { get; set; }

        public string PopupText { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public int UserID { get; set; }

        public virtual ltl_FeatureAttachment ltl_FeatureAttachment { get; set; }
    }
}
