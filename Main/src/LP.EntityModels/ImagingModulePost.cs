namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImagingModulePost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImagingModulePost()
        {
            ImageModulePostBlocks = new HashSet<ImageModulePostBlock>();
        }

        public int ImagingModulePostId { get; set; }

        public int PostID { get; set; }

        [StringLength(15)]
        public string Culture { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(256)]
        public string Subtitle { get; set; }

        public int? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageModulePostBlock> ImageModulePostBlocks { get; set; }
    }
}
