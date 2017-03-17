using System.Collections.Generic;
using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Model.Mappers
{
    public static class OverviewGroupProgressResponseContractEx
    {
        public static List<DashboardBarChartViewModel> ToViewModels(
            this OverviewGroupTypeProgressResponseContract overviewGroupTypeProgressResponseContract)
        {
            if (overviewGroupTypeProgressResponseContract == null ||
                overviewGroupTypeProgressResponseContract.DashboardBarChartContracts == null) return null;

            var dashboardBarChartViewModels = new List<DashboardBarChartViewModel>();

            foreach (var dashboardBarChartContract in overviewGroupTypeProgressResponseContract.DashboardBarChartContracts)
            {
                dashboardBarChartViewModels.Add(new DashboardBarChartViewModel
                {
                    Title = dashboardBarChartContract.Title,
                    NumberOfUsersCertified = dashboardBarChartContract.NumberOfUsersCertified,
                    NumberOfUsersInProgress = dashboardBarChartContract.NumberOfUsersInProgress,
                    NumberOfUsersStarted = dashboardBarChartContract.NumberOfUsersStarted,
                    PercentageOfUsersCertified = dashboardBarChartContract.PercentageOfUsersCertified,
                    PercentageOfUsersInProgress = dashboardBarChartContract.PercentageOfUsersInProgress,
                    PercentageOfUsersStarted = dashboardBarChartContract.PercentageOfUsersStarted
                });
            }

            return dashboardBarChartViewModels;
        }
    }
}
