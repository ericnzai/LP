using LP.Model.ViewModels.FeatureAttachments;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentPostInformationEx
    {
        public static FeatureAttachmentPostInformationViewModel ToViewModel(
            this FeatureAttachmentPostInformation featureAttachmentPostInformation)
        {
            return new FeatureAttachmentPostInformationViewModel
            {
                GroupName = featureAttachmentPostInformation.GroupName,
                PostTitle = featureAttachmentPostInformation.PostTitle,
                PostUrl = featureAttachmentPostInformation.PostUrl,
                SectionTitle = featureAttachmentPostInformation.SectionTitle,
                ParentSectionTitle = featureAttachmentPostInformation.ParentSectionTitle
            };
        }
    }
}
