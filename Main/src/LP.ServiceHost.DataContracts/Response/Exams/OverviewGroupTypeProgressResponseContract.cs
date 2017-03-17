using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class OverviewGroupTypeProgressResponseContract
    {
        public OverviewGroupTypeProgressResponseContract()
        {
            DashboardBarChartContracts = new List<DashboardBarChartContract>();
        }

        public List<DashboardBarChartContract> DashboardBarChartContracts { get; set; }
    }
}
