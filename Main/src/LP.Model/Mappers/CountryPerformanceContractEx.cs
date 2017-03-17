using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.Mappers
{
    public static class CountryPerformanceContractEx
    {
        public static CountryPerformanceViewModel ToViewModel(this CountryPerformanceContract countryPerformanceContract)
        {
            if (countryPerformanceContract == null) return null;

            //var performanceGroupTypeViewModel = new List<PerformanceGroupTypeViewModel>();
            //foreach (var performanceGroupTypeContract in countryPerformanceContract.PerformanceGroupTypeContract)
            //{
            //    performanceGroupTypeViewModel.Add(new PerformanceGroupTypeViewModel
            //    {
            //        Id = performanceGroupTypeContract.Id,
            //        Name = performanceGroupTypeContract.Name,
            //        TrainingTranslation = performanceGroupTypeContract.TrainingTranslation
            //    });
            //}

            var individualCountryPerformanceViewModels = new List<IndividualCountryPerformanceViewModel>();


      

                foreach (var countryPerformanceCultureContract in countryPerformanceContract.CountryPerformanceCultureContracts)
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

                        countryPerformanceCultureViewModel.Add(new CountryPerformanceCultureViewModel
                        {
                            NumberOfUsersWithAccess = countryPerformanceCultureContract.NumberOfUsersWithAccess,
                            Culture = countryPerformanceCultureContract.Culture,
                            CountryPerformanceGroupViewModels = countryPerformanceGroupViewModel
                        });

                        individualCountryPerformanceViewModels.Add(new IndividualCountryPerformanceViewModel
                        {
                            CountryName = countryPerformanceGroupContract.,
                            TotalNumberOfUsers = countryPerformanceSingleContract.TotalNumberOfUsers,
                            CountryPerformanceCultureViewModels = countryPerformanceCultureViewModel
                        });
                    }

                    
                }

                
            

            return new CountryPerformanceViewModel
            {
                PerformanceGroupTypeViewModel = performanceGroupTypeViewModel,
                CountryPerformanceSingleViewModel = individualCountryPerformanceViewModels
            };
        }
    }
}
