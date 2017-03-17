using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class LatestNewsItem
    {
        public string Content { get; set; }
        public string Date { get; set; }
        public int NewsId { get; set; }
        public TranslatedItem ReadMoreTranslatedItem { get; set; }

        public string ReadMoreUrl
        {
            get { return string.Format("news/#{0}", NewsId); }
        }
    }
}
