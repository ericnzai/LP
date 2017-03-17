using System.Collections.Generic;
using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.Model.Mappers
{
    public static class PerformanceGroupTypeContractEx
    {
        public static List<PerformanceGroupTypeViewModel> ToViewModels(this IEnumerable<PerformanceGroupTypeContract> performanceGroupTypeContracts)
        {
            var performanceGroupTypeViewModels = new List<PerformanceGroupTypeViewModel>();

            foreach (var performanceGroupTypeContract in performanceGroupTypeContracts)
            {
                if (performanceGroupTypeContract == null) return null;

                performanceGroupTypeViewModels.Add(new PerformanceGroupTypeViewModel
                {
                    GroupTypeId = performanceGroupTypeContract.GroupTypeId,
                    GroupTypeName = performanceGroupTypeContract.GroupTypeName,
                });
            }

            return performanceGroupTypeViewModels;
        }
    }
}
