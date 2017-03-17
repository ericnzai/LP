namespace LP.Api.Shared.Interfaces.BusinessLayer.Authentication
{
    public interface IAskAuthenticationApiBusiness
    {
        IUserCommands UserCommands { get; }
        IAuthenticationCommands AuthenticationCommands { get; }
    }
}
