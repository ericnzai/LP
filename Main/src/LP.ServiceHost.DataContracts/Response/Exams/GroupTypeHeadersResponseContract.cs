using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class GroupTypeHeadersResponseContract
    {
        public GroupTypeHeadersResponseContract()
        {
            PerformanceGroupTypeContract = new List<PerformanceGroupTypeContract>();
        }

        public List<PerformanceGroupTypeContract> PerformanceGroupTypeContract { get; set; }
    }
}
