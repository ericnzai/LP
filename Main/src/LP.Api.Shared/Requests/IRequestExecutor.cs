using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace LP.Api.Shared.Requests
{
    public interface IRequestExecutor
    {
        Task<HttpResponseMessage> ExecutePostAsync<T>(string uri, T value, ContentType contentType,
           bool disableErrorChecking = false);
    }
}
