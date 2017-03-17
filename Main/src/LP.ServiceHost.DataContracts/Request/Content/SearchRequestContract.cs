namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class SearchRequestContract
    {
        public string SearchTerm { get; set; }
        public string GroupTypeId { get; set; }
        public string TopicIds { get; set; }
    }
}
