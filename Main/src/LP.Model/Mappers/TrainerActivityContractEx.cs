using LP.Model.ViewModels.Dashboards;
using LP.Model.ViewModels.Dashboards.Trainer;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;

namespace LP.Model.Mappers
{
    public static class TrainerActivityContractEx
    {
        public static TrainerActivityViewModel ToViewModel(this TrainerActivityContract trainerActivityContract)
        {
            var trainerActivityViewModel = new TrainerActivityViewModel();

            if (trainerActivityContract == null) return trainerActivityViewModel;

                var traineeActivityLanguageViewModel = new List<TraineeActivityLanguageViewModel>();

            if (trainerActivityContract.TraineeActivityLanguageContract != null)
            {
                foreach (var traineeActivityLanguageContract in trainerActivityContract.TraineeActivityLanguageContract)
                {
                    traineeActivityLanguageViewModel.Add(traineeActivityLanguageContract.ToViewModel());
                }
            }

            trainerActivityViewModel.TraineeActivityLanguageViewModels = traineeActivityLanguageViewModel;
            trainerActivityViewModel.TraineeUserName = trainerActivityContract.TraineeUserName;

            return trainerActivityViewModel;
        }
    }
}
