using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace LP.Api.Shared.Binding
{
    public class HttpContentBinding : IHttpContentBinding
    {
        public T DeserialiseJson<T>(HttpContent content) where T : class, new()
        {
            var result = content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(result);
        }

        public HttpContent SerialiseToJson<T>(T type) where T : class, new()
        {
            var content = new ObjectContent(typeof(T), type, new JsonMediaTypeFormatter());

            return content;
        }

        public void CheckResponse(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
        }
    }
}