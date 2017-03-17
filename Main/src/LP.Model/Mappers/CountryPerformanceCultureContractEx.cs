using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using System.Collections.Generic;

namespace LP.Model.Mappers
{
    public static class CountryPerformanceCultureContractEx
    {
        public static List<CountryPerformanceCultureViewModel> ToViewModels(this IEnumerable<CountryPerformanceCultureContract> countryPerformanceCultureContracts)
        {
            var countryPerformanceCultureViewModels = new List<CountryPerformanceCultureViewModel>();

            if (countryPerformanceCultureContracts == null) return countryPerformanceCultureViewModels;

            foreach (var countryPerformanceCultureContract in countryPerformanceCultureContracts)
            {
                if (countryPerformanceCultureContract == null) continue;

                var countryPerformanceGroupViewModel = new List<CountryPerformanceGroupViewModel>();

                foreach (var countryPerformanceGroupContract in countryPerformanceCultureContract.CountryPerformanceGroupContract)
                {
                    countryPerformanceGroupViewModel.Add(new CountryPerformanceGroupViewModel
                    {
                        GroupTypeId = countryPerformanceGroupContract.GroupTypeId,
                        NumberOfUsersCertified = countryPerformanceGroupContract.NumberOfUsersCertified,
                        NumberOfUsersWithAccess = countryPerformanceGroupContract.NumberOfUsersWithAccess
                    });
                }

                countryPerformanceCultureViewModels.Add(new CountryPerformanceCultureViewModel
                {
                    NumberOfUsersWithAccess = countryPerformanceCultureContract.NumberOfUsersWithAccess,
                    CultureDescription = countryPerformanceCultureContract.CultureDescription,
                    CultureCode = countryPerformanceCultureContract.CultureCode,
                    CountryPerformanceGroupViewModels = countryPerformanceGroupViewModel
                });
            }

            return countryPerformanceCultureViewModels;

        }
    }
}
