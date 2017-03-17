using System;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class TopicTranslationContract
    {
        public int TopicId { get; set; }
        public string Culture { get; set; }
        public string CultureDisplayName { get; set; }
        public bool IsTranslated { get; set; }
        public Status Status { get; set; }

        public string TopicName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedByUserName { get; set; }
    }
}
