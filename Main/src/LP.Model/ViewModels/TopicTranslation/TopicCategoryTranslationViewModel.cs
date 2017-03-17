using System;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class TopicCategoryTranslationViewModel
    {
        public int TopicCategoryId { get; set; }
        public string Culture { get; set; }
        public bool IsTranslated { get; set; }

        public string TopicCategoryName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedByUserName { get; set; }

        public List<TopicTranslationViewModel> TopicTranslations { get; set; }

        public Status Status { get; set; }

        public int SortOrder { get; set; }
    }
}
