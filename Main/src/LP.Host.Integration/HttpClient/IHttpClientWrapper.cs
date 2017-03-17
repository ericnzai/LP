using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace LP.Host.Integration.HttpClient
{
    internal interface IHttpClientWrapper : IDisposable
    {
        /// <summary>
        /// Base address for api calls
        /// </summary>
        Uri BaseAddress { get; set; }
        /// <summary>
        /// Sets default http request headers
        /// </summary>
        HttpRequestHeaders DefaultRequestHeaders { get; }

        HttpResponseMessage Delete(string uri, bool disableErrorChecking = false);
        HttpResponseMessage Delete(string uri, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        /// <summary>
        ///  call to api with Get http verb
        /// </summary>
        /// <param name="uri">Relative uri appended to the BaseAddres may also contain querystring values</param>
        /// <returns>HttpResponseMessage</returns>
        HttpResponseMessage Get(string uri, bool disableErrorChecking = false);
        HttpResponseMessage Get(string uri, ContentType contentType, bool disableErrorChecking = false);
        HttpResponseMessage Get(string uri, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        HttpResponseMessage Post<T>(string uri, T value, bool disableErrorChecking = false);
        HttpResponseMessage Post<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false);
        HttpResponseMessage Post<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        HttpResponseMessage Put<T>(string uri, T value, bool disableErrorChecking = false);
        HttpResponseMessage Put<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false);
        HttpResponseMessage Put<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false);
        HttpResponseMessage PostWithTimeout<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, TimeSpan timeout);
        /// <summary>
        ///  call to api with Get http verb
        /// </summary>
        /// <param name="Uri">HttpRequestMessage</param>
        /// <returns>HttpResponseMessage</returns>
        HttpResponseMessage Send(HttpRequestMessage request, bool disableErrorChecking = false);
        HttpResponseMessage SendPost<T>(string uri, bool disableErrorChecking = false) where T : class, new();
    }
}
