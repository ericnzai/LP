using System.Collections.Generic;

namespace LP.Model.ViewModels.Dashboards
{
    public class DashboardBarChartViewModel
    {
        public string Title { get; set; }

        private string _trainingTranslation;

        public string TrainingTranslation
        {
            get
            {
                return string.IsNullOrEmpty(_trainingTranslation) ? "Training" : _trainingTranslation;
            }
            set
            {
                _trainingTranslation = value;
            }
        }

        public int NumberOfUsersStarted { get; set; }
        public int PercentageOfUsersStarted { get; set; }
        public int NumberOfUsersInProgress { get; set; }
        public int PercentageOfUsersInProgress { get; set; }
        public int NumberOfUsersCertified { get; set; }
        public int PercentageOfUsersCertified { get; set; }
    }
}
