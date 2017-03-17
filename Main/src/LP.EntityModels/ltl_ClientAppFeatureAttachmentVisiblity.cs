namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_ClientAppFeatureAttachmentVisiblity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeatureAttachmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientAppID { get; set; }

        public int ShowHide { get; set; }

        public virtual ClientApp ClientApp { get; set; }

        public virtual ltl_FeatureAttachment ltl_FeatureAttachment { get; set; }
    }
}
