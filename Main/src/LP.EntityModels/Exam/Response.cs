using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Responses")]
    public partial class Response
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Response()
        {
            ResponsesAnswers = new HashSet<ResponsesAnswer>();
        }

        public int Id { get; set; }

        public int AttemptId { get; set; }

        public byte StatusId { get; set; }

        public int QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        public short CBMValue { get; set; }

        public virtual Attempt Attempt { get; set; }

        public virtual Question Question { get; set; }

        public Status Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponsesAnswer> ResponsesAnswers { get; set; }
    }
}
