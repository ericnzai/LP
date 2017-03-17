using LP.ServiceHost.DataContracts.Enums;

namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.Attempts")]
    public partial class Attempt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attempt()
        {
            QuestionsOrders = new HashSet<QuestionsOrder>();
            Responses = new HashSet<Response>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public short ExamId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime AttemptStarted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime AttemptFinished { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }

        public byte StatusId { get; set; }

        public bool AttemptPassed { get; set; }

        public int GroupId { get; set; }

        public virtual AttemptDetail AttemptDetail { get; set; }

        public virtual Exam Exam { get; set; }

        public Status Status { get; set; }

        public virtual CertificatesAchieved CertificatesAchieved { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionsOrder> QuestionsOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Response> Responses { get; set; }
    }
}
