namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class TopicUpdateRequestContract
    {
        public int TopicId { get; set; }
        public int CategoryId { get; set; }
        public string TopicName { get; set; }
    }
}
