using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class OverviewCountryProgressResponseContract
    {
        public OverviewCountryProgressResponseContract()
        {
            CountryPerformanceContracts = new List<CountryPerformanceContract>();
        }

        public List<CountryPerformanceContract> CountryPerformanceContracts { get; set; }
    }
}
