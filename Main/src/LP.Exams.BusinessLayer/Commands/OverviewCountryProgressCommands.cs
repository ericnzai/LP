using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Response.Exams;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;

namespace LP.Exams.BusinessLayer.Commands
{
    public class OverviewCountryProgressCommands : IOverviewCountryProgressCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IFilterAllowedUser _allowedUserFilter;
        private readonly ICertificatesAchievedCommands _certificatesAchievedCommands;
        private readonly IGroupTypeCommands _groupTypeCommands;
        private readonly IRoleProvider _roleProvider;
        private readonly IFilterAllowedGroups _filterAllowedGroups;
        public OverviewCountryProgressCommands(IBaseCommands baseCommands, 
            IFilterAllowedUser allowedUserFilter,
            ICertificatesAchievedCommands certificatesAchievedCommands, 
            IGroupTypeCommands groupTypeCommands, 
            IRoleProvider roleProvider, IFilterAllowedGroups filterAllowedGroups)
        {
            _baseCommands = baseCommands;
            _allowedUserFilter = allowedUserFilter;
            _certificatesAchievedCommands = certificatesAchievedCommands;
            _groupTypeCommands = groupTypeCommands;
            _roleProvider = roleProvider;
            _filterAllowedGroups = filterAllowedGroups;
        }

        public async Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressResponseContract()
        {
            var countries = await _baseCommands.GetAllAsync<Country>();

            var orderedCountries = countries.OrderBy(c => c.Ranking).ThenBy(c=>c.CountryName);

            var listCountryPerformanceContract = await GetCountryPerformanceContractListByCountryAsync(orderedCountries);

            var overviewGroupTypeProgressResponseContract = new OverviewCountryProgressResponseContract
            {
                CountryPerformanceContracts = listCountryPerformanceContract
            };

            return overviewGroupTypeProgressResponseContract;
        }

        public async Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressFilteredByJobFunction(
            string jobRolesIds)
        {
            List<string> jobRoles =jobRolesIds.Split(',').ToList();
            List<int> jobFunctionIds = jobRoles.Select(j => int.Parse(j)).ToList();

            var countries = await _baseCommands.GetAllAsync<Country>();

            var orderedCountries = countries.OrderBy(c => c.Ranking).ThenBy(c => c.CountryName);

            var listCountryPerformanceContract = await GetCountryPerformanceContractListByCountryFilteredByJobFunctionAsync(orderedCountries, jobFunctionIds);

            var overviewGroupTypeProgressResponseContract = new OverviewCountryProgressResponseContract
            {
                CountryPerformanceContracts = listCountryPerformanceContract
            };

            return overviewGroupTypeProgressResponseContract;
        }
        public async Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressForRegionResponseContract(int regionId)
        {
            var countries = await _baseCommands.GetConditionalAsync<Country>(x => x.RegionId == regionId);

            var orderedCountries = countries.OrderBy(c => c.Ranking).ThenBy(c=>c.CountryName);

            var listCountryPerformanceContract = await GetCountryPerformanceContractListByCountryAsync(orderedCountries);

            var overviewGroupTypeProgressResponseContract = new OverviewCountryProgressResponseContract
            {
                CountryPerformanceContracts = listCountryPerformanceContract
            };

            return overviewGroupTypeProgressResponseContract;
        }

        public async Task<OverviewCountryProgressResponseContract>
            GetOverviewCountryProgressForRegionFilteredResponseContract(string jobRoleIds, int regionId)
        {
            List<string> jobRoles = jobRoleIds.Split(',').ToList();
            List<int> jobFunctionIds = jobRoles.Select(j => int.Parse(j)).ToList();

            var countries = await _baseCommands.GetConditionalAsync<Country>(x => x.RegionId == regionId);

            var orderedCountries = countries.OrderBy(c => c.Ranking).ThenBy(c => c.CountryName);

            var listCountryPerformanceContract = await GetCountryPerformanceContractListByCountryFilteredByJobFunctionAsync(orderedCountries, jobFunctionIds);

            var overviewGroupTypeProgressResponseContract = new OverviewCountryProgressResponseContract
            {
                CountryPerformanceContracts = listCountryPerformanceContract
            };

            return overviewGroupTypeProgressResponseContract;
        }
        public async Task<GroupTypeHeadersResponseContract> GetGroupTypeTableHeader()
        {
            var performanceGroupTypeContracts = new List<PerformanceGroupTypeContract>();

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            foreach (var groupType in groupTypes)
            {
                var performanceGroupTypeContract = new PerformanceGroupTypeContract
                {
                    GroupTypeId = groupType.ID,
                    GroupTypeName = groupType.Name
                };
                performanceGroupTypeContracts.Add(performanceGroupTypeContract);
            }

            return new GroupTypeHeadersResponseContract{PerformanceGroupTypeContract = performanceGroupTypeContracts};
        }

        private List<CertificatesAchieved> _certificatesAchieved;
        private List<User> _liveUsersNotHiddenFromReports;
        private async Task<List<CountryPerformanceContract>> GetCountryPerformanceContractListByCountryAsync(IEnumerable<Country> countries)
        {
            var listCountryPerformanceContract = new List<CountryPerformanceContract>();

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            var userCultureRoles = await _roleProvider.GetAllUserCultureRolesAsync();

            var userCultureRolesList = await userCultureRoles.ToListAsync();

            var allowedGroups = await _filterAllowedGroups.GetAllLiveGroups();

            var allowedGroupsList = await allowedGroups.ToListAsync();

            var groupTypesGrouped = allowedGroupsList.GroupBy(g => g.ltl_GroupType).ToList();

            var certificatesAchieved = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            _certificatesAchieved = await certificatesAchieved.ToListAsync();

            var liveUsersNotHiddenFromReports = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReports();

            _liveUsersNotHiddenFromReports = await liveUsersNotHiddenFromReports.ToListAsync();

            foreach (var country in countries)
            {
                var countryUsers = _allowedUserFilter.GetUsersFilteredByCountry(country.CountryID, _liveUsersNotHiddenFromReports).ToList();

                if (!countryUsers.Any()) continue;
                var countryPerformanceContract = GetCountryStatistics(country, userCultureRolesList, groupTypes, groupTypesGrouped, countryUsers);
                listCountryPerformanceContract.Add(countryPerformanceContract);
            }

            return listCountryPerformanceContract;

        }

        private async Task<List<CountryPerformanceContract>>
            GetCountryPerformanceContractListByCountryFilteredByJobFunctionAsync(IEnumerable<Country> countries,
                List<int> jobFunctionIds)
        {
            var listCountryPerformanceContract = new List<CountryPerformanceContract>();

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            var userCultureRoles = await _roleProvider.GetAllUserCultureRolesAsync();

            var userCultureRolesList = await userCultureRoles.ToListAsync();

            var allowedGroups = await _filterAllowedGroups.GetAllLiveGroups();

            var allowedGroupsList = await allowedGroups.ToListAsync();

            var groupTypesGrouped = allowedGroupsList.GroupBy(g => g.ltl_GroupType).ToList();

            var certificatesAchieved = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            _certificatesAchieved = await certificatesAchieved.ToListAsync();

            var liveUsersNotHiddenFromReports = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReports();

            _liveUsersNotHiddenFromReports = await liveUsersNotHiddenFromReports.ToListAsync();

            foreach (var country in countries)
            {
                //var countryUsers = _allowedUserFilter.GetUsersFilteredByCountry(country.CountryID, _liveUsersNotHiddenFromReports).ToList();
                var countryUsers = await _allowedUserFilter.GetUserIdsFilteredByJobFunctions(country.CountryID, jobFunctionIds);
                if (!countryUsers.Any()) continue;
                var countryPerformanceContract = GetCountryStatistics(country, userCultureRolesList, groupTypes, groupTypesGrouped, countryUsers);
                listCountryPerformanceContract.Add(countryPerformanceContract);
            }

            return listCountryPerformanceContract;
        }
        private CountryPerformanceContract GetCountryStatistics(Country country, IEnumerable<UserRole> userCultureRoles, List<ltl_GroupType> groupTypes, List<IGrouping<ltl_GroupType, Group>> groupTypesGrouped, List<User> countryUsers)
        {
            var countryUserIdsList = countryUsers.Select(u => u.UserID).ToList();

            var countryUsersCount = countryUserIdsList.Count();

            var countryUsersCultureRoles = userCultureRoles.Where(a => countryUserIdsList.Contains(a.UserID)).ToList();

            var countryUsersCultureRoleIds = countryUsersCultureRoles.Select(r => r.RoleID).Distinct();

            var countryPerformanceCultureContracts = new List<CountryPerformanceCultureContract>();
         
            var certificatesAchievedForCountryUsers = _certificatesAchieved.Where(a => countryUserIdsList.Contains(a.UserId)).ToList();
            
            foreach (var countryUsersCultureRoleId in countryUsersCultureRoleIds)
            {
                var cultureRoleId = countryUsersCultureRoleId;

                var cultureRole = countryUsersCultureRoles.FirstOrDefault(x => x.RoleID == countryUsersCultureRoleId);

                var userRoles =  countryUsers.SelectMany(cu => cu.askCore_UsersRoles);

                var userIdsForCulture = userRoles.Where(ur => ur.RoleID == cultureRoleId).Select(uc => uc.UserID);

                var countryUsersForCulture = countryUsers.Where(a => userIdsForCulture.Contains(a.UserID)).Select(u => u.UserID).ToList();
                
                if (cultureRole == null) continue;

                var listCountryPerformanceGroupContract = new List<CountryPerformanceGroupContract>();
                
                var numberOfUsersWithAccessForCulture = countryUsersForCulture.Count();
                
                foreach (var groupType in groupTypes)
                {
                    var groupTypeEntity = groupTypesGrouped.FirstOrDefault(a => a.Key.ID == groupType.ID);
                    if(groupTypeEntity == null) continue;
                    
                    var groupIds = groupTypeEntity.SelectMany(a => a.ltl_GroupPermissions).Where(a => a.RoleID == cultureRoleId).Select(g => g.GroupID);

                    var certifiedUsers = _certificatesAchievedCommands.GetNumberOfUsersCertififedForGroupTypeByCountry(groupType.ID,
                            groupIds, certificatesAchievedForCountryUsers);


                    var countryPerformanceGroupContract = new CountryPerformanceGroupContract
                    {
                        NumberOfUsersWithAccess = numberOfUsersWithAccessForCulture,
                        NumberOfUsersCertified = certifiedUsers,
                        GroupTypeId = groupType.ID
                    };

                    listCountryPerformanceGroupContract.Add(countryPerformanceGroupContract);
                }

                var cultureDisplayName = string.Empty;
                var cultureCode = string.Empty;

                if (cultureRole.askCore_Roles != null)
                {
                    cultureDisplayName = cultureRole.askCore_Roles.Description;
                    cultureCode = cultureRole.askCore_Roles.RoleName;
                }

                var countryPerformanceCultureContract = new CountryPerformanceCultureContract
                {
                    CultureDescription = cultureDisplayName,
                    CultureCode = cultureCode,
                    NumberOfUsersWithAccess = numberOfUsersWithAccessForCulture,
                    CountryPerformanceGroupContract = listCountryPerformanceGroupContract
                };

                countryPerformanceCultureContracts.Add(countryPerformanceCultureContract);
            }

            var countryPerformanceContract = new CountryPerformanceContract
            {
                CountryName = country.CountryName,
                    TotalNumberOfUsers = countryUsersCount,
                    CountryPerformanceCultureContracts = countryPerformanceCultureContracts,
                    CountryId = country.CountryID
            };
            
            return countryPerformanceContract;
        }
    }
}
