using System;

namespace LP.ServiceHost.DataContracts.Common.Content
{
    public class SearchItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrainingModules { get; set; }
        public string GroupName { get; set; }
        public string ParentSectionName { get; set; }
        public string Name { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string TrainingAreaFriendlyName { get; set; }
        public string GroupFriendlyName { get; set; }
        public string SectionFriendlyName { get; set; }
        public int SortOrder { get; set; }
    }
}
