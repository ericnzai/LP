using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.Model.Mappers
{
    public static class CountryPerformanceGroupContractEx
    {
        public static CountryPerformanceGroupViewModel ToViewModel(this CountryPerformanceGroupContract countryPerformanceGroupContract)
        {
            if (countryPerformanceGroupContract == null) return null;

            return new CountryPerformanceGroupViewModel
            {
                GroupTypeId = countryPerformanceGroupContract.GroupTypeId,
                NumberOfUsersCertified = countryPerformanceGroupContract.NumberOfUsersCertified,
                NumberOfUsersWithAccess = countryPerformanceGroupContract.NumberOfUsersWithAccess

            };
        }
    }
}
