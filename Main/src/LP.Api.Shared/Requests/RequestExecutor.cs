using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using LP.Api.Shared.HttpClient;
using LP.Api.Shared.Mime;

namespace LP.Api.Shared.Requests
{
    public class RequestExecutor : IRequestExecutor
    {
        public async Task<HttpResponseMessage> ExecutePostAsync<T>(string uri, T value, ContentType contentType, bool disableErrorChecking = false)
        {
            using (var client = new HttpClientWrapperAsync())
            {
                return await client.PostAsync(uri, value, new ContentType { MediaType = MediaTypes.Application.FormUrlEncoded });
            }
        }
    }
}