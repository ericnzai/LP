using System;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class TopicCategoryTranslationContract
    {
        public int TopicCategoryId { get; set; }
        public Status Status { get; set; }
        public string Culture { get; set; }
        public string CultureDisplayName { get; set; }
        public bool IsTranslated { get; set; }

        public int SortOrder { get; set; }
        public string TopicCategoryName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedByUserName { get; set; }

        public List<TopicTranslationContract> TopicTranslations { get; set; }
    }
}
