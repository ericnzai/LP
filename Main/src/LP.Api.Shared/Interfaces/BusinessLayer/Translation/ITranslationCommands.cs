using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Translation;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Translation
{
    public interface ITranslationCommands
    {
        Task<TranslationResponseContract> GetTranslatedItems(TranslationRequestContract translationRequestContract);
    }
}
