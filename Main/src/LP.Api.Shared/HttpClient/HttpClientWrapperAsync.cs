using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using LP.Api.Shared.Extensions;
using LP.Api.Shared.Mime;

namespace LP.Api.Shared.HttpClient
{
    public class HttpClientWrapperAsync : IHttpClientWrapperAsync
    {
        private readonly System.Net.Http.HttpClient _client;

        public HttpClientWrapperAsync()
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

        public async Task<HttpResponseMessage> GetAsync(string uri, bool disableErrorChecking = false)
        {
            var result = await _client.GetAsync(uri, HttpCompletionOption.ResponseContentRead);
           
            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, ContentType contentType, bool disableErrorChecking = false)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType.MediaType));

            return await GetAsync(uri, disableErrorChecking);
        }

        public async Task<HttpResponseMessage> GetAsync(string uri, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return await GetAsync(uri, contentType, disableErrorChecking);
        }

        public  async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool disableErrorChecking = false)
        {
            var response = _client.SendAsync(request);
            var result = await response;
            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> SendAsyncPost<T>(string uri, bool disableErrorChecking = false) where T : class, new()
        {
            var type = new T();

            var request = CreatePostRequest(type.GetType(), uri);

            var response = await SendAsync(request, disableErrorChecking);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsyncWithTimeout<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, TimeSpan timeout)
        {
            _client.Timeout = timeout;

            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return await PostAsync(uri, value, contentType);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T value, bool disableErrorChecking = false)
        {
            var result = await _client.PostAsync(uri, Content(value.GetType()));

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            
            return result;
        }

        public async virtual Task<HttpResponseMessage> PostAsync<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false)
        {
            HttpResponseMessage result;

            if (contentType.MediaType == MediaTypes.Application.Json)
            {
                result = await _client.PostAsJsonAsync(uri, value);
            }
            else if (contentType.MediaType == MediaTypes.Application.FormUrlEncoded)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                var kvp = value as List<KeyValuePair<string, string>>;

                request.Content = new FormUrlEncodedContent(kvp);

                result = await _client.SendAsync(request);
            }
            else
            {
                result = await _client.PostAsync(uri, Content(value.GetType()));
            }

            //if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return await PostAsync(uri, value, contentType, disableErrorChecking);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T value, bool disableErrorChecking = false)
        {
            var result = await _client.PutAsync(uri, Content(value.GetType()));

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false)
        {
            HttpResponseMessage result;
            if (contentType.MediaType == MediaTypes.Application.Json)
            {
                result = await _client.PutAsJsonAsync(uri, value);
            }
            else
            {
                result = await _client.PutAsync(uri, Content(value.GetType()));
            }


            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T value, ContentType contentType, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return await PutAsync(uri, value, contentType, disableErrorChecking);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri, bool disableErrorChecking = false)
        {
            var result = await _client.DeleteAsync(uri);

            if (disableErrorChecking == false) { result.ValidateSuccess(); }
            return result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri, IEnumerable<KeyValuePair<string, string>> customHeaders, bool disableErrorChecking = false)
        {
            customHeaders.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value));

            return await DeleteAsync(uri, disableErrorChecking);
        }

        private static HttpRequestMessage CreatePostRequest(Type type, string uri)
        {
            return new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(uri),
                Content = Content(type),
            };
        }

        private static HttpContent Content(Type type)
        {
            var properties = type.GetProperties();

            var parameters = new List<KeyValuePair<string, string>>();

            properties.ToList().ForEach(property =>
                parameters.Add(new KeyValuePair<string, string>(property.Name, NormalisePropertyValue(property.GetValue(property, null)))));

            return new FormUrlEncodedContent(parameters);
        }

        private static string NormalisePropertyValue(object propertyValue)
        {
            return propertyValue == null ? string.Empty : propertyValue.ToString();
        }

        public void Dispose()
        {
            if (_client == null) return;

            _client.Dispose();
        }
    }
}