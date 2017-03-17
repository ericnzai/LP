namespace LP.ServiceHost.DataContracts.Response.Authentication
{
    public class OauthTokenResponseContract
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }
    }
}
