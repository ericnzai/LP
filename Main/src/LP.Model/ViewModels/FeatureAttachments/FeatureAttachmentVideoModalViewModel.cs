using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.FeatureAttachments
{
    public class FeatureAttachmentVideoModalViewModel
    {
        public int FeatureAttachmentId { get; set; }
        public FeatureAttachmentPostInformationViewModel FeatureAttachmentPostInformationViewModel { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TranslatedItem LinkToTextTranslatedItem { get; set; }
    }
}
