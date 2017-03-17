namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicCategoryName { get; set; }
        public int CategoryId { get; set; }
        public int SortOrder { get; set; }
        public bool Checked { get; set; }
    }
}
