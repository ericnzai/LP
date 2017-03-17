namespace LP.EntityModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ltl_ExtendedResults
    {
        [Key]
        public int ExtendedResultsID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserCulture { get; set; }

        [Required]
        [StringLength(200)]
        public string DisplayName { get; set; }

        public int? ManagerUserID { get; set; }

        [StringLength(200)]
        public string ManagerUserName { get; set; }

        [StringLength(50)]
        public string ManagerCulture { get; set; }

        [StringLength(200)]
        public string ManagerDisplayName { get; set; }

        public DateTime? AttemptDate { get; set; }

        public int AttemptNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string IndicationName { get; set; }

        public int QuizID { get; set; }

        [Required]
        [StringLength(200)]
        public string QuizName { get; set; }

        public int Score { get; set; }

        public int MaximumScore { get; set; }

        public double PercentageScore { get; set; }

        public int NumberOfQuestions { get; set; }

        public bool PassedAttempt { get; set; }

        public DateTime BeganExam { get; set; }

        public DateTime? FinishedExam { get; set; }

        public long? TimeSpentOnAttempt { get; set; }

        public long? TimeSpentOnIndication { get; set; }

        [StringLength(12)]
        public string SiteCulture { get; set; }

        [StringLength(12)]
        public string UserCountry { get; set; }

        [StringLength(12)]
        public string ManagerCountry { get; set; }

        public int? IndicationID { get; set; }

        public DateTime? StartedIndicationTraining { get; set; }

        [StringLength(12)]
        public string QuizCulture { get; set; }

        [StringLength(200)]
        public string CertificatePDF { get; set; }

        [StringLength(200)]
        public string UserRole { get; set; }

        [StringLength(200)]
        public string ModuleName { get; set; }

        public long? TimeSpentOnModule { get; set; }

        public DateTime? StartedModuleTraining { get; set; }

        public int? AttemptID { get; set; }

        public int? SectionID { get; set; }

        [StringLength(100)]
        public string ExamsThatUserPassedToGetCertificate { get; set; }

        public int? GroupID { get; set; }

        [StringLength(100)]
        public string GroupTypeName { get; set; }

        [StringLength(200)]
        public string UserCountryName { get; set; }

        [StringLength(200)]
        public string UserCultureName { get; set; }

        [StringLength(200)]
        public string ManagerCountryName { get; set; }

        [StringLength(200)]
        public string ManagerCultureName { get; set; }

        [StringLength(200)]
        public string QuizCultureName { get; set; }

        [StringLength(200)]
        public string SiteCultureName { get; set; }

        public bool CertificateAchieved { get; set; }

        public bool MigrationComplete { get; set; }
    }
}
