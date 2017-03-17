using System.Collections.Generic;
using System.Configuration;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using LP.Api.Shared.Binding;
using LP.Api.Shared.Controllers;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Mime;
using LP.Api.Shared.Requests;
using LP.Model.Authentication;
using Ninject;

namespace LP.Authentication.Controllers
{
    public class BaseApiController : SharedBaseApiController
    {
        protected readonly IAskAuthenticationApiBusiness AskAuthenticationApiBusiness;
        
        [Inject]
        public IRequestExecutor RequestExecutor { private get; set; }
        
        [Inject]
        public IHttpContentBinding HttpContentBinding { private get; set; }
  
        public  BaseApiController(IAskAuthenticationApiBusiness askAuthenticationApiBusiness)
        {
            AskAuthenticationApiBusiness = askAuthenticationApiBusiness;
        }

        protected async Task<AccessTokenModel> GetAccessToken(string userName, string password)
        {
            var authenticationDetails = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "username", HttpUtility.UrlEncode(userName)),
                new KeyValuePair<string, string>( "password", password),
                new KeyValuePair<string, string>( "grant_type", "password"),
            };

            var url = string.Format("{0}oauth/token", ConfigurationManager.AppSettings["InternalBaseUrl"]);

            var httpResponseMessage = await RequestExecutor.ExecutePostAsync(url, authenticationDetails, new ContentType { MediaType = MediaTypes.Application.FormUrlEncoded });

            var accessTokenModel = HttpContentBinding.DeserialiseJson<AccessTokenModel>(httpResponseMessage.Content);

            return accessTokenModel;
        }
    }
}
