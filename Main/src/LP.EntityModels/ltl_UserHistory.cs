namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_UserHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserHistory_UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UserHistory_FirstLogin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UserHistory_LastActivity { get; set; }

        public virtual User askCore_Users { get; set; }
    }
}
