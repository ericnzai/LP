using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class CountryPerformanceContract
    {
        public string CountryName { get; set; }
        public int TotalNumberOfUsers { get; set; }
        public int CountryId { get; set; }
        public List<CountryPerformanceCultureContract> CountryPerformanceCultureContracts { get; set; }
    }
}
