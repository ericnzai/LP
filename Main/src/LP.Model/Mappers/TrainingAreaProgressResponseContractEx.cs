using LP.Model.ViewModels.Dashboards.Student;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Model.Mappers
{
    public static class TrainingAreaProgressResponseContractEx
    {
        public static TrainingAreaProgressViewModel ToViewModel(this TrainingAreaProgressResponseContract trainingAreaProgressResponseContract, string startButtonText, string continueButtonText, string reviewButtonText)
        {
            if (trainingAreaProgressResponseContract == null) return null;

            var trainingAreaProgressViewModel = new TrainingAreaProgressViewModel
            {
                TrainingAreaId = trainingAreaProgressResponseContract.TrainingAreaId,
                TrainingAreaName = trainingAreaProgressResponseContract.TrainingAreaName
            };

            foreach (var groupProgressContract in trainingAreaProgressResponseContract.GroupProgressContracts)
            {
                trainingAreaProgressViewModel.GroupProgressViewModels.Add(groupProgressContract.ToViewModel(startButtonText, continueButtonText, reviewButtonText));
            }

            return trainingAreaProgressViewModel;
        }
    }
}
