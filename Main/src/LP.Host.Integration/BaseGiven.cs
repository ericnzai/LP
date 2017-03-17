using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using LP.Api.Shared.Binding;
using LP.Host.Integration.Behaviours.Authentication.UserInformation;
using LP.Host.Integration.Behaviours.Content.Glossary;
using LP.Host.Integration.Helpers;
using LP.Host.Integration.HttpClient;
using LP.Host.Integration.IoC;
using Ninject;
using NUnit.Framework;
using SpecsFor;
using SpecsFor.Configuration;

namespace LP.Host.Integration
{
    public class BaseGiven : SpecsFor<HttpClientWrapper>
    {
        protected string Uri;
        protected HttpResponseException HttpResponseException;
        protected Exception Exception;
        protected HttpResponseMessage Response;
        protected IEnumerable<KeyValuePair<string, string>> AdminAuthorisationHeader;
        protected IEnumerable<KeyValuePair<string, string>> UserAuthorisationHeader;
        protected HttpContentBinding HttpContentBinding;
        protected AuthorisationHelper AuthorisationHelperInstance = new AuthorisationHelper();
        protected IKernel NinjectKernel;


        [SetUpFixture]
        public class ComposingContextConfig : SpecsForConfiguration
        {
            public ComposingContextConfig()
            {
                WhenTesting<INeedANewlyCreatedUser>().EnrichWith<UserCreationBehaviour>(); 
                WhenTesting<INeedSomeGlossaryItemsWithAudio>().EnrichWith<GlossaryAudioCreationBehaviour>();
            }
        }

        protected void PrepareSut()
        {
            

            NinjectKernel = SetupNinjectDependencies.CreateKernel();

            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            HttpContentBinding = new HttpContentBinding();
            AdminAuthorisationHeader = AuthorisationHelperInstance.GetAuthorisationHeader();
          
            SUT.BaseAddress = new Uri(baseUrl);
        }

        protected IEnumerable<KeyValuePair<string, string>> CreateCustomAuthorizationHeader(string userName= "admin",
            string password = "123Hello")
        {

            var authorisationTokenModel = AuthorisationHelperInstance.GetAuthorisationToken(userName, password);

            Console.WriteLine("authorisationTokenModel");
            Console.WriteLine(authorisationTokenModel.AccessToken);
            Console.WriteLine(authorisationTokenModel.ExpiresIn);
            Console.WriteLine(authorisationTokenModel.StatusCode);
            
            return AuthorisationHelperInstance.GetCustomAuthorisationHeader(authorisationTokenModel.AccessToken);
        }
    }
}
