using System.Collections.Generic;

namespace LP.Model.ViewModels.Dashboards
{
    public class CountryPerformanceViewModel
    {
        public List<PerformanceGroupTypeViewModel> PerformanceGroupTypeViewModel { get; set; }
        public List<IndividualCountryPerformanceViewModel> IndividualCountryPerformanceViewModels { get; set; }

        private string _countryTableHeader;
        public string CountryTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_countryTableHeader) ? "Country" : _countryTableHeader;
            }
            set
            {
                _countryTableHeader = value;
            }
        }

        private string _totalUsersTableHeader;
        public string TotalUsersTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_totalUsersTableHeader) ? "Total users" : _totalUsersTableHeader;
            }
            set
            {
                _totalUsersTableHeader = value;
            }
        }

        private string _numberOfAccessToTableHeader;
        public string NumberOfAccessToTableHeader
        {
            get
            {
                return string.IsNullOrEmpty(_numberOfAccessToTableHeader) ? "No. of access to" : _numberOfAccessToTableHeader;
            }
            set
            {
                _numberOfAccessToTableHeader = value;
            }
        }

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
    }
}