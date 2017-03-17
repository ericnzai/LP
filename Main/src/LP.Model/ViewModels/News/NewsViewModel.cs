using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.News
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            LatestNewsItems = new List<LatestNewsItem>();
        }

        public List<LatestNewsItem> LatestNewsItems { get; set; }
        public TranslatedItem NewsAndAnnouncementsTranslatedItem { get; set; }
        public TranslatedItem ReadMoreTranslatedItem { get; set; }
    }
}
