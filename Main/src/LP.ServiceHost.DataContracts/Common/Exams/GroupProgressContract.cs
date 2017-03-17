using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Common.Exams
{
    public class GroupProgressContract
    {
        public string GroupName { get; set; }
        public int PercentageComplete { get; set; }
        public int GroupId { get; set; }
        public string Culture { get; set; }
        public int NumberOfChapters { get; set; }
        public string GroupUrl { get; set; }
        public string CurrentChapter { get; set; }
        public string ChapterUrl { get; set; }
        public TrainingStatus TrainingStatus { get; set; }
        public Status GroupStatus { get; set; }
        public string LanguageName { get; set; }
        public bool WasCertified { get; set; }
    }
}
