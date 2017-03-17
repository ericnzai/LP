namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_StickyNotes
    {
        [Key]
        [StringLength(50)]
        public string StickyNoteID { get; set; }

        public string Header { get; set; }

        public string Body { get; set; }

        public int UserID { get; set; }

        public int PostID { get; set; }

        public int StatusID { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public bool IsMinimized { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ltl_Posts ltl_Posts { get; set; }
    }
}
