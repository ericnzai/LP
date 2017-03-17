using System.Collections.Generic;
using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Model.Mappers
{
    public static class OverviewCountryProgressResponseContractEx
    {
        public static List<IndividualCountryPerformanceViewModel> ToViewModels(
            this OverviewCountryProgressResponseContract overviewCountryProgressResponseContract)
        {
            var individualCountryPerformanceViewModels = new List<IndividualCountryPerformanceViewModel>();

            foreach (var countryPerformanceContract in overviewCountryProgressResponseContract.CountryPerformanceContracts)
            {
                var individualCountryPerformanceViewModel = new IndividualCountryPerformanceViewModel
                {
                    CountryName = countryPerformanceContract.CountryName,
                    CountryId = countryPerformanceContract.CountryId,
                    CountryPerformanceCultureViewModels = countryPerformanceContract.CountryPerformanceCultureContracts.ToViewModels(),
                    TotalNumberOfUsers = countryPerformanceContract.TotalNumberOfUsers

                };

                individualCountryPerformanceViewModels.Add(individualCountryPerformanceViewModel);
            }

            return individualCountryPerformanceViewModels;
        }
    }
}
