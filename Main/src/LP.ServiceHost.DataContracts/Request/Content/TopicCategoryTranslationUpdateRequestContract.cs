using LP.ServiceHost.DataContracts.Enums;
using System;

namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class TopicCategoryTranslationUpdateRequestContract
    {
        public int TopicCategoryId { get; set; }
        public string Culture { get; set; }
        public Status Staus { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdatedByUserId { get; set; }
        public string TopicCategoryName { get; set; }
    }
}
