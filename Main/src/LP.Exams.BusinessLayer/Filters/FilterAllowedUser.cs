using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Exams.BusinessLayer.Filters
{
    public class FilterAllowedUser : IFilterAllowedUser
    {
        private readonly IBaseCommands _baseCommands;
        private Role _hideUserFromReportsRole;
        public FilterAllowedUser(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        private async Task<IEnumerable<User>> GetLiveUsers()
        {
            return await _baseCommands.GetConditionalAsync<User>(x => x.UserStatusID == (int)UserStatus.Active);
        }

        public async Task<IEnumerable<int>> GetAllLiveUserIds()
        {
            var liveUsers = await GetLiveUsers();

            return liveUsers.Select(a => a.UserID);
        }

        public async Task<IQueryable<User>> GetAllLiveUsersNotHiddenFromReports()
        {
            if (_hideUserFromReportsRole == null)
            {
                var roles = await _baseCommands.GetConditionalAsync<Role>(r => r.RoleName == "HideUserFromReports");

                _hideUserFromReportsRole = await roles.FirstOrDefaultAsync(r => r.RoleName == "HideUserFromReports");
            }

            var liveUsersNotHideUserFromReports = await _baseCommands.GetConditionalWithIncludesAsync<User>(u => u.UserStatusID == (int)UserStatus.Active
                && !u.askCore_UsersRoles.Select(r => r.RoleID).Contains(_hideUserFromReportsRole.RoleID), x => x.askCore_Countries, x => x.askCore_UsersRoles);

            return liveUsersNotHideUserFromReports;
        }

        public async Task<IEnumerable<int>> GetAllLiveUsersNotHiddenFromReportsIds()
        {
            var liveUsers = await GetAllLiveUsersNotHiddenFromReports();

            return liveUsers.Select(a => a.UserID);
        }

        public async Task<IEnumerable<int>> GetAllLiveUsersNotHiddenFromReportsAndWithoutFakeCountryIds()
        {
            var liveUsers = await GetAllLiveUsersNotHiddenFromReports();

            return liveUsers.Select(a => a.UserID);
        }

        public async Task<IEnumerable<User>> GetUsersFilteredByRegion(int regionId)
        {
            var regionCountryIds = _baseCommands.GetAll<Country>().Where(c => c.RegionId == regionId).Select(c => c.CountryID);
            var allowedUsers = await GetAllLiveUsersNotHiddenFromReports();

            return allowedUsers.Where(u => u.CountryID != null && regionCountryIds.Contains((int)u.CountryID));
        }

        public async Task<IEnumerable<int>> GetUserIdsFilteredByRegion(int regionId)
        {
            var allowedUsers = await GetUsersFilteredByRegion(regionId);

            return allowedUsers.Select(a => a.UserID);
        }

        public async Task<IEnumerable<User>> GetUsersFilteredByCountry(int countryId)
        {
            var allowedUsers = await GetAllLiveUsersNotHiddenFromReports();

            return allowedUsers.Where(u => u.CountryID == countryId);
        }

        public async Task<IEnumerable<User>> GetUsersFilteredByTrainer(int trainerId)
        {
            var allowedUsers = await GetAllLiveUsersNotHiddenFromReports();

            return allowedUsers.Where(u => u.ParentID == trainerId);
        }

        public async Task<List<int>> GetUserIdsFilteredByTrainer(int trainerId)
        {
            var allowedUsers = await GetUsersFilteredByTrainer(trainerId);

            return allowedUsers.Select(a => a.UserID).ToList();
        }

        public IEnumerable<User> GetUsersFilteredByCountry(int countryId, IEnumerable<User> users)
        {
            return users.Where(u => u.CountryID == countryId);
        }

        public async Task<List<int>> GetUserIdsFilteredByCountry(int countryId)
        {
            var allowedUsers = await GetUsersFilteredByCountry(countryId);

            return allowedUsers.Select(a => a.UserID).ToList();
        }

        public async Task<int> GetUserCountTotal()
        {
            var allowedUsers = await GetAllLiveUsersNotHiddenFromReports();

            return allowedUsers.Count();
        }

        public async Task<int> GetUserCountByTrainer(int trainerId)
        {
            var allowedUsers = await GetUsersFilteredByTrainer(trainerId);

            return allowedUsers.Count();
        }

        public async Task<int> GetUserCountByRegion(int regionId)
        {
            var allowedUsers = await GetUsersFilteredByRegion(regionId);

            return allowedUsers.Count();
        }

        public async Task<IEnumerable<int>> GetFilteredUserIdsNotHiddenFromReports(int regionId, int countryId, int jobRoleId, int trainerId)
        {
            var users = await GetAllLiveUsersNotHiddenFromReports();

            if (regionId > 0)
            {
                users = users.Where(region => region.askCore_Countries.RegionId == regionId);
            }

            if (countryId > 0)
            {
                users = users.Where(c => c.CountryID == countryId);
            }

            if (jobRoleId > 0)
            {
                users = users.Where(ur => ur.askCore_UsersRoles.Select(a => a.RoleID).Contains(jobRoleId));
            }

            if (trainerId > 0)
            {
                users = users.Where(u => u.ParentID == trainerId);
            }

            return users.Select(u => u.UserID);
        }

        public async Task<IEnumerable<int>> GetFilteredUserIdsNotHiddenFromReports(int trainerId)
        {
            var users = await GetAllLiveUsersNotHiddenFromReports();

            if (trainerId > 0)
            {
                users = users.Where(u => u.ParentID == trainerId);
            }

            return users.Select(u => u.UserID);
        }

        public async Task<Dictionary<int,int?>> GetUserandParentIdsFilteredByCountryId(int countryId)
        {
            var allowedUsers = await GetUsersFilteredByCountry(countryId);

            var userParentDict = allowedUsers.ToDictionary(u=>u.UserID,p=>p.ParentID);

            return userParentDict;
        }

        public async Task<List<User>> GetUserIdsFilteredByJobFunctions(int countryId, List<int> jobFunctionIds)
        {
            var users = await GetAllLiveUsersNotHiddenFromReports();

            users = users.Where(u => u.CountryID == countryId);

            users =
               users.Where(s => s.askCore_UsersRoles.Any(ur => jobFunctionIds.Contains(ur.RoleID)));

            return users.ToList();
        }

        public async Task<List<User>> GetUsersFilteredByTrainerAndJobFunctions(int trainerId, List<int> jobFunctionIds)
        {
            var allowedUsers = await GetUsersFilteredByTrainer(trainerId);

            allowedUsers = allowedUsers.Where(s => s.askCore_UsersRoles.Any(ur => jobFunctionIds.Contains(ur.RoleID)));

            return allowedUsers.ToList();
        }
    }
}
