using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IFeatureAttachmentCommands
    {
        Task<FeatureAttachmentModalResponseContract> GetFeatureAttachmentModalResponseContract(
            int featureAttachmentId, UserDetails userDetails);

        Task<FeatureAttachmentVideoModalResponseContract> GetFeatureAttachmentVideoModalResponseContract(
            int featureAttachmentId, UserDetails userDetails);

        Task<FeatureAttachmentPageResponseContract> GetFeatureAttachmentPageAsync(int pageNumber, UserDetails userDetails);
    }
}
