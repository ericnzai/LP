using Newtonsoft.Json;

namespace LP.Model.Authentication
{
    public class AccessTokenModel
    {
        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }
        [JsonProperty("Expires_In")]
        public int TokenExpiry { get; set; }
    }
}
