namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_History
    {
        [Key]
        public int HistoryID { get; set; }

        public int UserID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public int? HistoryTypeID { get; set; }

        public int? Value { get; set; }

        public string Notes { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public string BrowserDetails { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }

        public int? GroupID { get; set; }

        public int? ModuleTypeID { get; set; }

        public int? SectionID { get; set; }

        public virtual ltl_HistoryType ltl_HistoryType { get; set; }
    }
}
