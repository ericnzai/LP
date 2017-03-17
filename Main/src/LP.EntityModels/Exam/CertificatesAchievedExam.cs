namespace LP.EntityModels.Exam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.CertificatesAchievedExams")]
    public partial class CertificatesAchievedExam
    {
        public int Id { get; set; }

        public int CertificateAchievedId { get; set; }

        public short ExamId { get; set; }

        public virtual CertificatesAchieved CertificatesAchieved { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
