using System.Linq;
using LP.Model.ViewModels.FeatureAttachments;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentPageResponseContractEx
    {
        public static FeatureAttachmentPageViewModel ToViewModel(
            this FeatureAttachmentPageResponseContract featureAttachmentPageResponseContract)
        {
            return new FeatureAttachmentPageViewModel
            {
                FeatureAttachments = featureAttachmentPageResponseContract.FeatureAttachmentItems.Select(f=> f.ToViewModel()).ToList()
                //Description = featureAttachmentModalResponseContract.Description,
                //ImageUrl = featureAttachmentModalResponseContract.ImageUrl,
                //Title = featureAttachmentModalResponseContract.Title,
                //FeatureAttachmentPostInformationViewModel = featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.ToViewModel(),
                //FeatureAttachmentId = featureAttachmentModalResponseContract.FeatureAttachmentId
            };
        }
    }
}
