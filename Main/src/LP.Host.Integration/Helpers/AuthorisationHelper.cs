using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using LP.Api.Shared.Binding;
using LP.Api.Shared.HttpClient;
using LP.Api.Shared.Mime;
using LP.Host.Integration.HttpClient;
using Newtonsoft.Json;

namespace LP.Host.Integration.Helpers
{
    public class AuthorisationHelper
    {
        private const string AuthorisationHeaderKey = "Authorization";

        public AuthorisationTokenModel GetAuthorisationToken(string userName = "admin", string password = "123Hello")
        {
            using (var client = new HttpClientWrapper())
            {
                var authenticationDetails = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "username", HttpUtility.UrlEncode(userName)),
                    new KeyValuePair<string, string>( "password", password),
                    new KeyValuePair<string, string>( "grant_type", "password"),
                };

                var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

                var tokenRequest = client.Post(baseUrl + "oauth/token", authenticationDetails, new ContentType { MediaType = MediaTypes.Application.FormUrlEncoded }, true);

                var httpContentBinding = new HttpContentBinding();

                Console.WriteLine("tokenRequest:");
                Console.WriteLine(tokenRequest.Content.ReadAsStringAsync().Result);

                var authorisationTokenModel = httpContentBinding.DeserialiseJson<AuthorisationTokenModel>(tokenRequest.Content);
                authorisationTokenModel.StatusCode = tokenRequest.StatusCode;
 
                return authorisationTokenModel;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetAuthorisationHeader()
        {
            var authorisationToken = GetAuthorisationToken();

            return new List<KeyValuePair<string, string>> 
            { 
                new KeyValuePair<string, string>(AuthorisationHeaderKey, string.Format("bearer {0}", authorisationToken.AccessToken))
            };
        }

        public IEnumerable<KeyValuePair<string, string>> GetCustomAuthorisationHeader(string token)
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
            
            return new List<KeyValuePair<string, string>> 
            { 
                new KeyValuePair<string, string>(AuthorisationHeaderKey, string.Format("bearer {0}", token))
            };
        }

        public class AuthorisationTokenModel
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            public HttpStatusCode StatusCode { get; set; }
            [JsonProperty("Expires_In")]
            public int ExpiresIn { get; set; }
            [JsonProperty("Token_Type")]
            public string TokenType { get; set; }
        }
    }
}
