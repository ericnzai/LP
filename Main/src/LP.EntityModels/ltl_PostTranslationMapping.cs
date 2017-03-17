namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_PostTranslationMapping
    {
        [Key]
        public int PostTranslationMappingId { get; set; }

        public int SourcePostId { get; set; }

        [Required]
        [StringLength(13)]
        public string SourceCulture { get; set; }

        public int SourceSectionID { get; set; }

        public int DestinationPostId { get; set; }

        [Required]
        [StringLength(13)]
        public string DestinationCulture { get; set; }

        public int DestinationSectionId { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }

        public virtual ltl_Sections ltl_Sections { get; set; }
    }
}
