using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface ICultureMenuCommands
    {
        Task<CompleteCultureMenuResponseContract> GetAvailableCultures(UserDetails userDetails);
        Task<CompleteCultureMenuResponseContract> GetAvailableCulturesExceptEnglishGlobal(UserDetails userDetails);
    }
}
