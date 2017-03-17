using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.Mappers
{
    public static class CountryPerformanceSingleContractEx
    {
        public static IndividualCountryPerformanceViewModel ToViewModel(this CountryPerformanceSingleContract countryPerformanceSingleContract)
        {
            if (countryPerformanceSingleContract == null) return null;

            var countryPerformanceCultureViewModel = new List<CountryPerformanceCultureViewModel>();

            foreach (var countryPerformanceCultureContract in countryPerformanceSingleContract.CountryPerformanceCultureContract)
            {
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

                countryPerformanceCultureViewModel.Add(new CountryPerformanceCultureViewModel
                {
                    NumberOfUsersWithAccess = countryPerformanceCultureContract.NumberOfUsersWithAccess,
                    Culture = countryPerformanceCultureContract.Culture,
                    CountryPerformanceGroupViewModels = countryPerformanceGroupViewModel
                });
            }

            return new IndividualCountryPerformanceViewModel
            {
                CountryName = countryPerformanceSingleContract.CountryName,
                TotalNumberOfUsers = countryPerformanceSingleContract.TotalNumberOfUsers,
                CountryPerformanceCultureViewModels = countryPerformanceCultureViewModel
            };
        }
    }
}
