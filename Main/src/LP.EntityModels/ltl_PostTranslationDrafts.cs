namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_PostTranslationDrafts
    {
        [Key]
        public int PostDraftID { get; set; }

        public int PostTranslationID { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public int DraftStatusTypeID { get; set; }

        [StringLength(256)]
        public string Subject { get; set; }

        public string Body { get; set; }

        [StringLength(256)]
        public string PostName { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public virtual ltl_DraftStatusType ltl_DraftStatusType { get; set; }
    }
}
