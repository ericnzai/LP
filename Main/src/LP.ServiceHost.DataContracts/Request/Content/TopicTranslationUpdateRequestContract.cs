using System;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class TopicTranslationUpdateRequestContract
    {
        public int TopicId { get; set; }
        public string Culture { get; set; }
        public Status Staus { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdatedByUserId { get; set; }
        public string TopicName { get; set; }
    }
}
