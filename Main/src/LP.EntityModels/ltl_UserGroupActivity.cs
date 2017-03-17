namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_UserGroupActivity
    {
        [Key]
        public int uga_Id { get; set; }

        public int uga_UserId { get; set; }

        public int uga_GroupId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime uga_StartDateTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime uga_LastActiveDatetime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime uga_Modified { get; set; }
    }
}
