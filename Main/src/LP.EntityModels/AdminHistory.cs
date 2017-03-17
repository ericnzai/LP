namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("ltl_AdminHistory")]
    public partial class AdminHistory
    {
        [Key]
        public int AdminHistoryID { get; set; }

        public int UserID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public int HistoryTypeID { get; set; }

        public string PreValue { get; set; }

        public string PostValue { get; set; }

        public string Action { get; set; }

        public string Notes1 { get; set; }

        public string Notes2 { get; set; }

        public bool? Active { get; set; }

        [StringLength(13)]
        public string Culture { get; set; }
    }
}
