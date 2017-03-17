namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageModulePostBlock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImageModulePostBlock()
        {
            ImageModulePostBlockPopups = new HashSet<ImageModulePostBlockPopup>();
        }

        public int ImageModulePostBlockId { get; set; }

        [StringLength(256)]
        public string LeftImage { get; set; }

        [StringLength(256)]
        public string LeftImageTitle { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        [StringLength(256)]
        public string RightImage { get; set; }

        [StringLength(256)]
        public string RightImageTitle { get; set; }

        public bool IsComparison { get; set; }

        public int Status { get; set; }

        public int ImagingModulePost_ImagingModulePostId { get; set; }

        public bool IsVideoBlock { get; set; }

        [StringLength(256)]
        public string LeftVideo { get; set; }

        [StringLength(512)]
        public string LeftVideoTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImageModulePostBlockPopup> ImageModulePostBlockPopups { get; set; }

        public virtual ImagingModulePost ImagingModulePost { get; set; }
    }
}
