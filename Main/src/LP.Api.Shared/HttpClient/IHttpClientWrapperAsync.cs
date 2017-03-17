using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LP.Api.Shared.HttpClient
{
    public interface IHttpClientWrapperAsync : IDisposable
    {
        /// <summary>
        /// Base address for api calls
        /// </summary>
        Uri BaseAddress { get; set; }
        /// <summary>
        /// Sets default http request headers
        /// </summary>
        HttpRequestHeaders DefaultRequestHeaders { get; }

        Task<HttpResponseMessage> DeleteAsync(string uri, bool disableErrorChecking = false);
        Task<HttpResponseMessage> DeleteAsync(string uri, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);

        /// <summary>
        /// Async call to api with Get http verb
        /// </summary>
        /// <param name="uri">Relative uri appended to the BaseAddres may also contain querystring values</param>
        /// <param name="disableErrorChecking"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> GetAsync(string uri, bool disableErrorChecking = false);
        Task<HttpResponseMessage> GetAsync(string uri, ContentType contentType, bool disableErrorChecking = false);
        Task<HttpResponseMessage> GetAsync(string uri, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T value, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T value, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PutAsync<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        Task<HttpResponseMessage> PostAsyncWithTimeout<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, TimeSpan timeout);

        /// <summary>
        /// Async call to api with Get http verb
        /// </summary>
        /// <param name="request"></param>
        /// <param name="disableErrorChecking"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool disableErrorChecking = false);
        Task<HttpResponseMessage> SendAsyncPost<T>(string uri, bool disableErrorChecking = false) where T : class, new();
    }
}
