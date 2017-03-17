using System.Collections.Generic;

namespace LP.Model.ViewModels.Dashboards
{
    public class CountryPerformanceCultureViewModel
    {
        public int NumberOfUsersWithAccess { get; set; }

        public string NumberOfUsersWithAccessText
        {
            get { return string.Format("{0} {1}", CultureDescription, NumberOfUsersWithAccess); }
        }

        public string CultureDescription { get; set; }
        public string CultureCode { get; set; }
        public List<CountryPerformanceGroupViewModel> CountryPerformanceGroupViewModels { get; set; }
    }

}
