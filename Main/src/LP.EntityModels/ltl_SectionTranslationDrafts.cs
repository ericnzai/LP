namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SectionTranslationDrafts
    {
        [Key]
        public int SectionDraftID { get; set; }

        public int SectionID { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public int DraftStatusTypeID { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string LearningObjectives { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public virtual ltl_DraftStatusType ltl_DraftStatusType { get; set; }

        public virtual ltl_SectionTranslations ltl_SectionTranslations { get; set; }
    }
}
