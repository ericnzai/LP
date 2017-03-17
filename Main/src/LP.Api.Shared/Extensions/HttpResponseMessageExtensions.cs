using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LP.Api.Shared.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static void ValidateSuccess(this HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode) return;

            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized || responseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpResponseException(responseMessage.StatusCode);
            }

            throw new HttpResponseException(responseMessage.StatusCode);
        }
    }
}
