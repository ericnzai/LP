using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country
{
    public class CountryActivitiesContract
    {
        public List<PerformanceGroupTypeContract> PerformanceGroupTypeContract { get; set; }
        public List<CountryActivityContract> CountryActivityContract { get; set; }

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

        private string _trainerTableHeader;

        public string TrainerTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_trainerTableHeader) ? "Trainer" : _trainerTableHeader;
            }
            set
            {
                _trainerTableHeader = value;
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
