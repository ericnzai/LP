using LP.Model.ViewModels.Dashboards.Student;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.Mappers
{
    public static class GroupProgressContractEx
    {
        public static GroupProgressViewModel ToViewModel(this GroupProgressContract groupProgressContract, string startButtonText, string continueButtonText, string reviewButtonText)
        {
            if (groupProgressContract == null) return null;

            var groupProgressViewModel = new GroupProgressViewModel
            {
                GroupName = groupProgressContract.GroupName,
                PieChartViewModel = new PieChartViewModel {Id = groupProgressContract.GroupId, PercentageFilled = groupProgressContract.PercentageComplete},
                GroupId = groupProgressContract.GroupId,
                Culture = groupProgressContract.Culture,
                NumberOfChapters = groupProgressContract.NumberOfChapters,
                GroupUrl = groupProgressContract.GroupUrl,
                CurrentChapter = groupProgressContract.CurrentChapter,
                ChapterUrl = groupProgressContract.ChapterUrl,
                TrainingStatus = groupProgressContract.TrainingStatus,
                GroupStatus = groupProgressContract.GroupStatus,
                LanguageName = groupProgressContract.LanguageName,
                WasCertified = groupProgressContract.WasCertified
            };

            switch (groupProgressContract.TrainingStatus)
            {
                    case TrainingStatus.NotStarted:
                    groupProgressViewModel.ChapterProgressButtonText = startButtonText;
                    break;
                    case TrainingStatus.InProgress:
                    groupProgressViewModel.ChapterProgressButtonText = continueButtonText;
                    break;
                    case TrainingStatus.Completed:
                    groupProgressViewModel.ChapterProgressButtonText = reviewButtonText;
                    break;
            }

            return groupProgressViewModel;
        }
    }
}
