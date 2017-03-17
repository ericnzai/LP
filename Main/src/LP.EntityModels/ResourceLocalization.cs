namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Resource_Localization")]
    public partial class ResourceLocalization
    {
        [Key]
        public int pk { get; set; }

        [Required]
        [StringLength(100)]
        public string ResourceId { get; set; }

        public string Value { get; set; }

        [StringLength(10)]
        public string LocaleId { get; set; }

        [StringLength(100)]
        public string ResourceSet { get; set; }

        [StringLength(512)]
        public string Type { get; set; }

        public byte[] BinFile { get; set; }

        public string TextFile { get; set; }

        [StringLength(128)]
        public string Filename { get; set; }

        [StringLength(512)]
        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int? UserID { get; set; }

        [StringLength(100)]
        public string PageType { get; set; }

        public byte? RequiredContentPlaceholders { get; set; }

        public string PreviewUrl { get; set; }

        public bool IsCultureDependent { get; set; }

        [StringLength(100)]
        public string PreviewUrlDisplay { get; set; }

        public virtual User askCore_Users { get; set; }
    }
}
