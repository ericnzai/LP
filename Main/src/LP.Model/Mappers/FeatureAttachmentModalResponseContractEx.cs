using LP.Model.ViewModels.FeatureAttachments;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentModalResponseContractEx
    {
        public static FeatureAttachmentModalViewModel ToViewModel(
            this FeatureAttachmentModalResponseContract featureAttachmentModalResponseContract)
        {
            return new FeatureAttachmentModalViewModel
            {
                Description = featureAttachmentModalResponseContract.Description,
                ImageUrl = featureAttachmentModalResponseContract.ImageUrl,
                Title = featureAttachmentModalResponseContract.Title,
                PopupText = featureAttachmentModalResponseContract.PopupText,
                FeatureAttachmentPostInformationViewModel = featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.ToViewModel(),
                FeatureAttachmentId = featureAttachmentModalResponseContract.FeatureAttachmentId
            };
        }
    }
}
