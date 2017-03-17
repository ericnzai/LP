using System;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Model.Authentication;

namespace LP.Authentication.BusinessLayer.Commands
{
    public class AuthenticationCommands : IAuthenticationCommands
    {
        public Task<AccessTokenModel> GenerateAccessToken(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
