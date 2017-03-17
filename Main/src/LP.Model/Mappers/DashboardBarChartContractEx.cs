using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.Model.Mappers
{
    public static class DashboardBarChartContractEx
    {
        public static DashboardBarChartViewModel ToViewModel(this DashboardBarChartContract dashboardBarChartContract)
        {
            if (dashboardBarChartContract == null) return null;

            return new DashboardBarChartViewModel
            {
                Title = dashboardBarChartContract.Title,
                NumberOfUsersCertified = dashboardBarChartContract.NumberOfUsersCertified,
                NumberOfUsersInProgress = dashboardBarChartContract.NumberOfUsersInProgress,
                NumberOfUsersStarted = dashboardBarChartContract.NumberOfUsersStarted,
                PercentageOfUsersCertified = dashboardBarChartContract.PercentageOfUsersCertified,
                PercentageOfUsersInProgress = dashboardBarChartContract.PercentageOfUsersInProgress,
                PercentageOfUsersStarted = dashboardBarChartContract.PercentageOfUsersStarted
            };
        }
    }
}
