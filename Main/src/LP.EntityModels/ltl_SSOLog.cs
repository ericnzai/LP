namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_SSOLog
    {
        [Key]
        public int SSOLogID { get; set; }

        public DateTime? Date { get; set; }

        public string Message { get; set; }
    }
}
