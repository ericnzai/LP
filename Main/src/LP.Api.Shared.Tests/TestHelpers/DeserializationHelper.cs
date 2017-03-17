using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using LP.Api.Shared.Binding;

namespace LP.Api.Shared.Tests.TestHelpers
{
    public static class DeserializationHelper
    {
        public async static Task<T> GetDeserializedResponseContent<T>(IHttpActionResult httpActionResult) where T : class, new()
        {
            var httpResponseMessage = await httpActionResult.ExecuteAsync(new CancellationToken());
            if (httpResponseMessage == null || httpResponseMessage.Content == null) return null;
            var httpContentBinding = new HttpContentBinding();
            return httpContentBinding.DeserialiseJson<T>(httpResponseMessage.Content);
        }
    }
}
