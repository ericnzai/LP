using LP.Model.ViewModels.Dashboards;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Model.Mappers
{
    public static class GroupTypeHeadersEx
    {
        public static List<PerformanceGroupTypeViewModel> ToViewModels(
            this GroupTypeHeadersResponseContract groupTypeHeadersResponseContract)
        {
            var performanceGroupTypeViewModelViewModels = new List<PerformanceGroupTypeViewModel>();

            foreach (var performanceGroupTypeContract in groupTypeHeadersResponseContract.PerformanceGroupTypeContract)
            {
                var performanceGroupTypeViewModel = new PerformanceGroupTypeViewModel
                {
                    
                    GroupTypeId = performanceGroupTypeContract.GroupTypeId,
                    GroupTypeName = performanceGroupTypeContract.GroupTypeName
                };

                performanceGroupTypeViewModelViewModels.Add(performanceGroupTypeViewModel);
            }

            return performanceGroupTypeViewModelViewModels;
        }
    }
}
