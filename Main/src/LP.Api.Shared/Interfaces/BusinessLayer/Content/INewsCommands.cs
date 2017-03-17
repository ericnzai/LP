using System.Globalization;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface INewsCommands
    {
        Task<LatestNewsResponseContract> GetLatestNewsAsync(CultureInfo currentRequestCultureInfo);
    }
}
