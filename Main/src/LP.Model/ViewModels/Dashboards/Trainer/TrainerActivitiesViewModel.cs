using System.Collections.Generic;
using LP.Model.ViewModels.Group;

namespace LP.Model.ViewModels.Dashboards.Trainer
{
    public class TrainerActivitiesViewModel
    {
        public List<PerformanceGroupTypeViewModel> PerformanceGroupTypeViewModels { get; set; }
        public List<TrainerActivityViewModel> TrainerActivityViewModels { get; set; }

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
