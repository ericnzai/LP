namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_HoverOver
    {
        [Key]
        public int HoverOverID { get; set; }

        public string Word { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool? FindPlural { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public bool? ForceCreate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int? UserID { get; set; }

        public bool TranslationRequired { get; set; }

        public int ParentID { get; set; }

        public int? AudioFileID { get; set; }

        public bool UseParentAudioFile { get; set; }

        public virtual User askCore_Users { get; set; }
        [ForeignKey("AudioFileID")]
        public virtual ltl_HoverOverAudio ltl_HoverOverAudio { get; set; }
    }
}
