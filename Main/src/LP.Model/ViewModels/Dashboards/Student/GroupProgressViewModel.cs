using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.Dashboards.Student
{
    public class GroupProgressViewModel
    {
        public GroupProgressViewModel()
        {
            PieChartViewModel = new PieChartViewModel();
        }

        public string GroupName { get; set; }
        public PieChartViewModel PieChartViewModel { get; set; }
        public int GroupId { get; set; }
        public string Culture { get; set; }
        public int NumberOfChapters { get; set; }
        public string GroupUrl { get; set; }
        public string CurrentChapter { get; set; }
        public string ChapterUrl { get; set; }
        public bool WasCertified { get; set; }

        public TrainingStatus TrainingStatus { get; set; }
        public Status GroupStatus { get; set; }
        public string LanguageName { get; set; }
        public bool IsCertificationComplete { get { return PieChartViewModel.PercentageFilled == 100; } }
        public string ChapterProgressButtonText { get; set; }
       

    }
}
