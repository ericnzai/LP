using System.Collections.Generic;
using LP.Model.ViewModels.Group;

namespace LP.Model.ViewModels.Dashboards.Country
{
    public class CountryActivitiesViewModel
    {
        public List<PerformanceGroupTypeViewModel> PerformanceGroupTypeViewModel { get; set; }
        public List<CountryActivityViewModel> CountryActivityViewModels { get; set; }

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
