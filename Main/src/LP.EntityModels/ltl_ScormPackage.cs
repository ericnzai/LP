namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_ScormPackage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostID { get; set; }

        [Required]
        public string ScormCourseID { get; set; }

        [Required]
        public string Title { get; set; }

        public int LayoutType { get; set; }

        public int NumberOfPages { get; set; }

        public int? ContentHeight { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }
    }
}
