using System;

namespace LP.EntityModels.StoredProcedure.Output
{
    public class SearchWithRowCount
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ParentSectionName { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string TrainingAreaFriendlyName { get; set; }
        public string GroupFriendlyName { get; set; }
        public string SectionFriendlyName { get; set; }
        public int SortOrder { get; set; }
    }
}
