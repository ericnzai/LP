using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;

namespace LP.Authentication.BusinessLayer
{
    public class AskAuthenticationApiBusiness : IAskAuthenticationApiBusiness
    {
        private readonly IUserCommands _userCommands;
        private readonly IAuthenticationCommands _authenticationCommands;
        public AskAuthenticationApiBusiness(IUserCommands userCommands, IAuthenticationCommands authenticationCommands)
        {
            _userCommands = userCommands;
            _authenticationCommands = authenticationCommands;
        }


        public IUserCommands UserCommands { get { return _userCommands; } }
        public IAuthenticationCommands AuthenticationCommands { get { return _authenticationCommands; } }
    }
}
