using LP.Model.ViewModels.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class TopicTranslationViewModel
    {
        public int TopicId { get; set; }
        public string Culture { get; set; }
        public bool IsTranslated { get; set; }

        public string TopicName { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedByUserName { get; set; }
    }
}
