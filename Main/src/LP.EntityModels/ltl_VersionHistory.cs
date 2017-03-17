namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_VersionHistory
    {
        [Key]
        public int VersionHistoryID { get; set; }

        [Required]
        [StringLength(200)]
        public string ScriptName { get; set; }

        public int Version1 { get; set; }

        public int Version2 { get; set; }

        public DateTime RunDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Comments { get; set; }

        public bool Success { get; set; }
    }
}
