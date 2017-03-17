using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Attributes;
using LP.Api.Shared.HttpClient;
using LP.Api.Shared.Mime;
using LP.Api.Shared.Providers;
using LP.PresentationLayer.Filters;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Translation;
using Newtonsoft.Json;

namespace LP.PresentationLayer.Controllers
{
    [Culture]
    [AskError]
    //[AllowCrossSiteJson]
    public class BaseController : Controller
    {
        private readonly HttpClientWrapperAsync _httpClient;

        protected BaseController()
        {
            _httpClient = new HttpClientWrapperAsync { BaseAddress = new Uri(ConfigurationManager.AppSettings["BackendServiceUri"]) };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected string CurrentCulture { get { return RouteData.Values["Culture"].ToString(); } }

        protected async Task<T> GetResponseFromService<T>(string serviceResourceName)
            where T : new()
        {
            return await GetResponseFromService<T>(serviceResourceName, null);
        }

        protected async Task<T> GetResponseFromService<T>(string serviceResourceName, string serviceResourceIdentifier) where T : new()
        {
            T responseDeserialized;
            var uri = serviceResourceName;
            if (!string.IsNullOrWhiteSpace(serviceResourceIdentifier))
            {
                uri += serviceResourceIdentifier;
            }

           

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
                new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
            };

            RemoveExistingHeaders();
            var response = await _httpClient.GetAsync(uri, new ContentType { MediaType = MediaTypes.Application.Json }, headers );
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                responseDeserialized = JsonConvert.DeserializeObject<T>(responseData);
            }
            else
            {
                responseDeserialized = new T();
            }
            return responseDeserialized;
        }

        protected void RemoveExistingHeaders()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }

            if (_httpClient.DefaultRequestHeaders.Contains("x-culture"))
            {
                _httpClient.DefaultRequestHeaders.Remove("x-culture");
            }
        }

        protected async Task<TResponse> PostRequestToService<TRequest, TResponse>(string serviceResourceName, TRequest requestContract) where TResponse : new() where TRequest : new()
        {
            TResponse responseDeserialized;
            var uri = serviceResourceName;
  
            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
                new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
            };
            RemoveExistingHeaders();
            var response = await _httpClient.PostAsync(uri, requestContract, new ContentType { MediaType = MediaTypes.Application.Json }, headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                responseDeserialized = new TResponse();
            }
            return responseDeserialized;
        }

        protected async Task<TResponse> PutRequestToService<TRequest, TResponse>(string serviceResourceName, TRequest requestContract)
            where TResponse : new()
            where TRequest : new()
        {
            TResponse responseDeserialized;
            var uri = serviceResourceName;

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
                new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
            };
            RemoveExistingHeaders();
            var response = await _httpClient.PutAsync(uri, requestContract, new ContentType { MediaType = MediaTypes.Application.Json }, headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                responseDeserialized = new TResponse();
            }
            return responseDeserialized;
        }
        protected async Task<TResponse> DeleteRequestToService<TResponse>(string serviceResourceName)
            where TResponse : new()
        {
            TResponse responseDeserialized;
            var uri = serviceResourceName;

            var headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
                new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
            };
            RemoveExistingHeaders();
            var response = await _httpClient.DeleteAsync(uri, headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                responseDeserialized = new TResponse();
            }
            return responseDeserialized;
        }

        protected static string GetTranslatedValueByResourceId(string resourceId, IEnumerable<TranslatedItem> translatedItems)
        {
            return GetTranslatedItemByResourceId(resourceId, translatedItems).TranslatedValue;
        }

        protected static TranslatedItem GetTranslatedItemByResourceId(string resourceId, IEnumerable<TranslatedItem> translatedItems)
        {
            var translatedItem =
                translatedItems.FirstOrDefault(
                    a => a.ResourceId == resourceId);

            return translatedItem ?? new TranslatedItem{ResourceId = resourceId};
        }

        protected static string GetDashboardTypeResourceSet(DashboardType dashboardType)
        {
            switch (dashboardType)
            {
                case DashboardType.CountryDashboard:
                    return TranslatedItemResourceSetProvider.CountryDashboard;
                case DashboardType.GlobalDashboard:
                    return TranslatedItemResourceSetProvider.GlobalDashboard;
                case DashboardType.RegionalDashboard:
                    return TranslatedItemResourceSetProvider.RegionalDashboard;
                case DashboardType.TrainerDashboard:
                    return TranslatedItemResourceSetProvider.TrainerDashboard;
            }

            return string.Empty;
        }

        protected static string GetDashboardTypeResourceSet(int dashboardType)
        {
            var dashboardTypeEnum = (DashboardType)dashboardType;

            return GetDashboardTypeResourceSet(dashboardTypeEnum);
        }

        //protected async Task<TResponse> PutRequestToService<TRequest, TResponse>(string serviceResourceName, string serviceResourceIdentifier, TRequest requestContract)
        //    where TResponse : new()
        //    where TRequest : new()
        //{
        //    TResponse responseDeserialized;
        //    var uri = serviceResourceName;
        //    if (!string.IsNullOrWhiteSpace(serviceResourceIdentifier))
        //    {
        //        uri += serviceResourceIdentifier;
        //    }

        //    var headers = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
        //        new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
        //    };
        //    RemoveExistingHeaders();
        //    var response = await _httpClient.PutAsync(uri, requestContract, new ContentType { MediaType = MediaTypes.Application.Json }, headers);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseData = await response.Content.ReadAsStringAsync();
        //        responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseData);
        //    }
        //    else
        //    {
        //        responseDeserialized = new TResponse();
        //    }
        //    return responseDeserialized;
        //}

        //protected async Task<TResponse> DeleteRequestToService<TResponse>(string serviceResourceName)
        //    where TResponse : new()
        //{
        //    TResponse responseDeserialized;
        //    var uri = serviceResourceName;
        
        //    var headers = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("x-culture", RouteData.Values["Culture"].ToString()),
        //        new KeyValuePair<string, string>("Authorization", string.Format("Bearer {0}", RouteData.Values["AUTH_TOKEN"]))
        //    };

        //    RemoveExistingHeaders();

        //    var response = await _httpClient.DeleteAsync(uri, headers);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseData = await response.Content.ReadAsStringAsync();
        //        responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseData);
        //    }
        //    else
        //    {
        //        responseDeserialized = new TResponse();
        //    }
        //    return responseDeserialized;
        //}

        protected async Task<List<TranslatedItem>> RequestTranslatedItems(List<TranslationRequest> translationRequests)
        {
            var translationRequestContract = new TranslationRequestContract { TranslationRequests = translationRequests, Culture = CurrentCulture };

            var translationResponseContract = await PostRequestToService<TranslationRequestContract, TranslationResponseContract>("api/translation/static", translationRequestContract);

            return translationResponseContract.TranslatedItems;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _httpClient.Dispose();
        }
        
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                            viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                                ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}