namespace LP.Model.ViewModels.Common
{
    public class ChapterInfoViewModel
    {
        public string ContinueUrl { get; set; }
        public int? NumberOfChapterCompleted { get; set; }
        public bool ModuleStarted { get; set; }
        public bool ModuleCompleted { get; set; }
        public bool IsUserCertifiedInModule { get; set; }
        public string CurrentChapterTitle { get; set; }
        public int PercentComplete { get; set; }
    }
}
