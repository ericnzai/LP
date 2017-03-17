using System.Collections.Generic;
using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Authentication
{
    public interface IUserCommands
    {
        Task<HttpResponseStatus> AuthenticateUserAsync(string userName, string password);
        Task<UserDetails> GetUserDetailsAsync(string username);
        Task<UserInformationResponseContract> GetUserInformationAsync(UserDetails userDetails);
        string GetUserNameByIdAsync(int userId);
        List<DecryptedUser> GetDecryptedUsers(IEnumerable<int> userIds);
        Task<int> GetCertificationOverviewUserCountTotal();
        Task<int> GetCertificationOverviewUserCountByRegion(int regionId);
        Task<int> GetUserCountFiltered(int regionId = 0, int countryId = 0, int jobRole = 0, int trainerId = 0);
        Task<int> GetCertificationOverviewUserCountTotalByTrainer(int trainerId);
    }
}
