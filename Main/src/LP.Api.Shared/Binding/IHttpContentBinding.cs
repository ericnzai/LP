using System.Net.Http;
using System.Threading.Tasks;

namespace LP.Api.Shared.Binding
{
    public interface IHttpContentBinding
    {
        T DeserialiseJson<T>(HttpContent content) where T : class, new();
        HttpContent SerialiseToJson<T>(T type) where T : class, new();
        void CheckResponse(HttpResponseMessage httpResponseMessage);
    }
}
