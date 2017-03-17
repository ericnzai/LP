namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class GlossaryItem
    {
        public int GlossaryItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrainingModules { get; set; }
        public bool HasAudio { get; set; }
    }
}
