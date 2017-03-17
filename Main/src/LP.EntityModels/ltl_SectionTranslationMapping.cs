namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SectionTranslationMapping
    {
        [Key]
        public int SectionTranslationMappingId { get; set; }

        public int SourceSectionId { get; set; }

        public int SourceGroupId { get; set; }

        [Required]
        [StringLength(13)]
        public string SourceCulture { get; set; }

        public int DestinationSectionId { get; set; }

        public int DestinationGroupId { get; set; }

        [Required]
        [StringLength(13)]
        public string DestinationCulture { get; set; }

        public virtual ltl_Sections ltl_Sections { get; set; }

        public virtual ltl_SectionTranslations ltl_SectionTranslations { get; set; }
    }
}
