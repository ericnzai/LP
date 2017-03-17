using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using LP.Api.Shared.Extensions;
using LP.Api.Shared.Mime;

namespace LP.Host.Integration.HttpClient
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly System.Net.Http.HttpClient _client;

        public HttpClientWrapper()
        {
            int timeoutInMinutes;
            var timeoutSetting = System.Configuration.ConfigurationManager.AppSettings["HttpClientRequestTimeout"];

            int.TryParse(timeoutSetting, out timeoutInMinutes);

            _client = new System.Net.Http.HttpClient
            {
                Timeout = timeoutInMinutes > 0 ? new TimeSpan(0, timeoutInMinutes, 0) : new TimeSpan(0, 30, 0)
            };
        }

        public Uri BaseAddress
        {
            get
            {
                return _client.BaseAddress;
            }
            set
            {
                _client.BaseAddress = value;
            }
        }

        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                return _client.DefaultRequestHeaders;
            }
        }

        public HttpResponseMessage Get(string uri, bool disableErrorChecking = false)
        {
            var request = _client.GetAsync(uri, HttpCompletionOption.ResponseContentRead);
            var result = request.Result;
            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage Get(string uri, ContentType contentType, bool disableErrorChecking = false)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType.MediaType));

            return Get(uri, disableErrorChecking);
        }

        public HttpResponseMessage Get(string uri, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            _client.DefaultRequestHeaders.Clear();
            
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return Get(uri, contentType, disableErrorChecking);
        }

        public HttpResponseMessage Send(HttpRequestMessage request, bool disableErrorChecking = false)
        {
            var response = _client.SendAsync(request);
            var result = response.Result;
            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage SendPost<T>(string uri, bool disableErrorChecking = false) where T : class, new()
        {
            var type = new T();

            var request = CreatePostRequest(type.GetType(), uri);

            var response = Send(request, disableErrorChecking);

            return response;
        }

        public HttpResponseMessage PostWithTimeout<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, TimeSpan timeout)
        {
            _client.Timeout = timeout;

            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return Post(uri, value, contentType);
        }

        public HttpResponseMessage Post<T>(string uri, T value, bool disableErrorChecking = false)
        {
            var result = _client.PostAsync(uri, Content(value.GetType())).Result;

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public virtual HttpResponseMessage Post<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false)
        {
            HttpResponseMessage result;
            
            if (contentType.MediaType == MediaTypes.Application.Json)
            {
                result = _client.PostAsJsonAsync(uri, value).Result;
            }
            else if (contentType.MediaType == MediaTypes.Application.FormUrlEncoded)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                var kvp = value as List<KeyValuePair<string, string>>;

                request.Content = new FormUrlEncodedContent(kvp);

                result = _client.SendAsync(request).Result;
            }
            else
            {
                result = _client.PostAsync(uri, Content(value.GetType())).Result;
            }

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage Post<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return Post(uri, value, contentType, disableErrorChecking);
        }

        public HttpResponseMessage Put<T>(string uri, T value, bool disableErrorChecking = false)
        {
            var result = _client.PutAsync(uri, Content(value.GetType())).Result;

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage Put<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false)
        {
            HttpResponseMessage result;
            if (contentType.MediaType == MediaTypes.Application.Json)
            { result = _client.PutAsJsonAsync(uri, value).Result; }
            else
            { result = _client.PutAsync(uri, Content(value.GetType())).Result; }


            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage Put<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return Put(uri, value, contentType, disableErrorChecking);
        }

        public HttpResponseMessage Delete(string uri, bool disableErrorChecking = false)
        {
            var result = _client.DeleteAsync(uri).Result;

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public HttpResponseMessage Delete(string uri, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return Delete(uri, disableErrorChecking);
        }

        private HttpRequestMessage CreatePostRequest(Type type, string uri)
        {
            return new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(uri),
                Content = Content(type),
            };
        }

        private HttpContent Content(Type type)
        {
            var properties = type.GetProperties();

            var parameters = new List<KeyValuePair<string, string>>();

            properties.ToList().ForEach(property =>
                parameters.Add(new KeyValuePair<string, string>(property.Name, NormalisePropertyValue(property.GetValue(property, null)))));

            return new FormUrlEncodedContent(parameters);
        }

        private static string NormalisePropertyValue(object propertyValue)
        {
            if (propertyValue == null) return "";

            return propertyValue.ToString();
        }

        public void Dispose()
        {
            if (_client == null) return;

            _client.Dispose();
        }
    }
}
