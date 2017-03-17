namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageModulePostBlockPopup
    {
        public int ImageModulePostBlockPopupId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(256)]
        public string NormalEyeImage { get; set; }

        [StringLength(256)]
        public string NormalEyeImageTitle { get; set; }

        public bool RequiresZoom { get; set; }

        public int ZoomPercentage { get; set; }

        public int ImageModulePostPopoutLayoutTypeId { get; set; }

        public int PopupType { get; set; }

        public int ImageModulePostBlock_ImageModulePostBlockId { get; set; }

        public int Status { get; set; }

        public virtual ImageModulePostBlock ImageModulePostBlock { get; set; }
    }
}
