using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class CountryPerformanceCultureContract
    {
        public int NumberOfUsersWithAccess { get; set; }
        public string DisplayLanguage { get; set; }
        public string CultureDescription { get; set; }
        public string CultureCode { get; set; }
        //public string Display { get { return string.Format("{0} {1}", DisplayLanguage, NumberOfUsersWithAccess); } }
        public List<CountryPerformanceGroupContract> CountryPerformanceGroupContract { get; set; }
    }
}
