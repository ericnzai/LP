using System.Threading.Tasks;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Authentication
{
    public interface IAuthenticationCommands
    {
        Task<AccessTokenModel> GenerateAccessToken(string userName, string password);
    }
}
