using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class FeatureAttachmentVideoModalResponseContract
    {
        public FeatureAttachmentVideoModalResponseContract()
        {
            FeatureAttachmentPostInformation = new FeatureAttachmentPostInformation();
        }

        public int FeatureAttachmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PopupText { get; set; }
        public FeatureAttachmentPostInformation FeatureAttachmentPostInformation { get; set; }
        
    }
}
