namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_EmbeddedContent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostID { get; set; }

        [Required]
        public string FolderName { get; set; }

        [Required]
        public string Title { get; set; }

        public int NumberOfPages { get; set; }

        public int? ContentHeight { get; set; }
    }
}
