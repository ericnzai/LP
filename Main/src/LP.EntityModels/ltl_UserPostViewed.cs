namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_UserPostViewed
    {
        [Key]
        public int upv_Id { get; set; }

        public short upv_UserId { get; set; }

        public short upv_GroupId { get; set; }

        public int? upv_SectionId { get; set; }

        public int upv_PostId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime upv_DateTime { get; set; }
    }
}
