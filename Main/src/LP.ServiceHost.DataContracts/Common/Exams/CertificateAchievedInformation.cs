using System;

namespace LP.ServiceHost.DataContracts.Common.Exams
{
    public class CertificateAchievedInformation
    {
        public string GroupName { get; set; }
        public string CertificateDisplayName { get; set; }
        public string CertificateFileName { get; set; }
        public DateTime AttemptDate { get; set; }
        public DateTime FinishedExam { get; set; }
    }
}
