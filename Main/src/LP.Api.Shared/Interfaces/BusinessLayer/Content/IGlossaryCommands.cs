using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IGlossaryCommands
    {
        Task<GlossaryItemsResponseContract> GetAllGlossaryItems(string culture);
        Task<GlossaryAudioResponseContract> GetGlossaryAudio(int glossaryItemId);
    }
}
