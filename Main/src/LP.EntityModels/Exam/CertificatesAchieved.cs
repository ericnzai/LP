namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.CertificatesAchieved")]
    public partial class CertificatesAchieved
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CertificatesAchieved()
        {
            CertificatesAchievedExams = new HashSet<CertificatesAchievedExam>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }

        public short CertificateId { get; set; }

        public bool IsObsolete { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTimeAchieved { get; set; }

        [Required]
        [StringLength(256)]
        public string GroupName { get; set; }

        public virtual Attempt Attempt { get; set; }

        public virtual Certificate Certificate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CertificatesAchievedExam> CertificatesAchievedExams { get; set; }
    }
}
