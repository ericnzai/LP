namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageModulePostPopoutLayoutType
    {
        public int ImageModulePostPopoutLayoutTypeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
