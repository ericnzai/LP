using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content.FeatureAttachment;

namespace LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment
{
    public class FeatureAttachmentPageResponseContract
    {
        public int PageNumber { get; set; }
        public List<FeatureAttachmentItemContract> FeatureAttachmentItems { get; set; }
    }
}
