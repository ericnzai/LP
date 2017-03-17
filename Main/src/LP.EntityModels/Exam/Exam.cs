using LP.ServiceHost.DataContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LP.EntityModels.Exam
{
    [Table("exam.Exams")]
    public partial class Exam
    {
        public Exam()
        {
            Attempts = new HashSet<Attempt>();
            CertificatesAchievedExams = new HashSet<CertificatesAchievedExam>();
            ExamsQuestions = new HashSet<ExamsQuestion>();
            TrainingsExams = new HashSet<TrainingsExam>();
        }

        public short Id { get; set; }

        public byte StatusId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public byte? Weight { get; set; }

        public int CreatedByUserId { get; set; }

        public int? LastUpdatedByUserId { get; set; }

        public short? CertificateId { get; set; }

        [Required]
        [StringLength(13)]
        public string Culture { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdated { get; set; }

        public virtual ICollection<Attempt> Attempts { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual ICollection<CertificatesAchievedExam> CertificatesAchievedExams { get; set; }

        public virtual ExamDetail ExamDetail { get; set; }

        public virtual ICollection<ExamsQuestion> ExamsQuestions { get; set; }

        public virtual ICollection<TrainingsExam> TrainingsExams { get; set; }
    }
}
