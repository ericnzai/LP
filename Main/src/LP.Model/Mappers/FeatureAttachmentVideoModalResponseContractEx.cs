using LP.Model.ViewModels.FeatureAttachments;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentVideoModalResponseContractEx
    {
        public static FeatureAttachmentVideoModalViewModel ToViewModel(this FeatureAttachmentVideoModalResponseContract featureAttachmentModalResponseContract)
        {
            return new FeatureAttachmentVideoModalViewModel
            {
                Description = featureAttachmentModalResponseContract.Description,
                Title = featureAttachmentModalResponseContract.Title,
                FeatureAttachmentPostInformationViewModel = featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.ToViewModel(),
                FeatureAttachmentId = featureAttachmentModalResponseContract.FeatureAttachmentId
            };
        }
    }
}
