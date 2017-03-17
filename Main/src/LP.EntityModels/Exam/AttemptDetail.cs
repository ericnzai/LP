namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.AttemptDetails")]
    public partial class AttemptDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? ManagerUserId { get; set; }

        public short AttemptNumber { get; set; }

        public short Score { get; set; }

        public short PercentageScore { get; set; }

        public virtual Attempt Attempt { get; set; }
    }
}
