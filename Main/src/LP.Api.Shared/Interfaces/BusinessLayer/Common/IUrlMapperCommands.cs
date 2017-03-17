using System.Threading.Tasks;
using LP.EntityModels;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface IUrlMapperCommands
    {
        Task<string> MapUrlForPost(int postId);
        string MapUrlForPost(ltl_Posts post);
        Task<string> MapUrlForFeatureAttachmentImage(ltl_FeatureAttachment featureAttachment, string currentCulture);
    }
}
