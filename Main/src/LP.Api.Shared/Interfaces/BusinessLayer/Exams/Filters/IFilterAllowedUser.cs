using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters
{
    public interface IFilterAllowedUser
    {
        Task<IEnumerable<int>> GetAllLiveUserIds();
        Task<IEnumerable<int>> GetAllLiveUsersNotHiddenFromReportsIds();
        Task<IQueryable<User>> GetAllLiveUsersNotHiddenFromReports();
        Task<IEnumerable<int>> GetUserIdsFilteredByRegion(int regionId);
        Task<int> GetUserCountTotal();
        Task<int> GetUserCountByRegion(int regionId);
        Task<List<int>> GetUserIdsFilteredByCountry(int countryId);
        Task<List<int>> GetUserIdsFilteredByTrainer(int trainerId);
        Task<IEnumerable<User>> GetUsersFilteredByTrainer(int trainerId);
        IEnumerable<User> GetUsersFilteredByCountry(int countryId, IEnumerable<User> users);

        Task<IEnumerable<int>> GetFilteredUserIdsNotHiddenFromReports(int regionId, int countryId, int jobRoleId,
            int trainerId);
        Task<int> GetUserCountByTrainer(int trainerId);
        Task<IEnumerable<int>> GetFilteredUserIdsNotHiddenFromReports(int trainerId);
        Task<Dictionary<int, int?>> GetUserandParentIdsFilteredByCountryId(int countryId);
        Task<List<User>> GetUserIdsFilteredByJobFunctions(int countryId, List<int> jobFunctionIds);
        Task<List<User>> GetUsersFilteredByTrainerAndJobFunctions(int trainerId, List<int> jobFunctionIds);
    }
}
