using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Response.Authentication
{
    public class LoginResponseContract
    {
        public HttpResponseStatus HttpResponseStatus { get; set; }
        public string AccessToken { get; set; }
        public int TokenExpiry { get; set; }
    }
}
