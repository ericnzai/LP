using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Answers")]
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            ResponsesAnswers = new HashSet<ResponsesAnswer>();
        }

        public int Id { get; set; }

        public byte StatusId { get; set; }

        [Column("Answer")]
        public string Answer1 { get; set; }

        public int QuestionId { get; set; }

        public byte Weight { get; set; }

        public bool IsCorrect { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdated { get; set; }

        public int? LastUpdatedByUserId { get; set; }

        public virtual Status Status { get; set; }

        public virtual Question Question { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsesAnswer> ResponsesAnswers { get; set; }
    }
}
