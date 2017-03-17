using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer
{
    public class TrainerActivitiesContract
    {
        public List<PerformanceGroupTypeContract> PerformanceGroupTypeContract { get; set; }
        public List<TrainerActivityContract> TrainerActivityContract { get; set; }

        private string _traineeTableHeader;
        public string TraineeTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_traineeTableHeader) ? "Trainee" : _traineeTableHeader;
            }
            set
            {
                _traineeTableHeader = value;
            }
        }

        private string _languageTableHeader;
        public string LanguageTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_languageTableHeader) ? "Language" : _languageTableHeader;
            }
            set
            {
                _languageTableHeader = value;
            }
        }
    }
}
