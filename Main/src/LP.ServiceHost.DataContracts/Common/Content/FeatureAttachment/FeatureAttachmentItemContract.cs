namespace LP.ServiceHost.DataContracts.Common.Content.FeatureAttachment
{
    public class FeatureAttachmentItemContract
    {
        public int FeatureAttachmentId { get; set; }

        public int FeatureAttachmentTypeId { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Parameters { get; set; }

        public int SortOrder { get; set; }

        public string Template { get; set; }
    }
}
