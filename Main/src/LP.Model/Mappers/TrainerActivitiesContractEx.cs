using LP.Model.ViewModels.Dashboards;
using LP.Model.ViewModels.Dashboards.Trainer;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;

namespace LP.Model.Mappers
{
    public static class TrainerActivitiesContractEx
    {
        public static TrainerActivitiesViewModel ToViewModel(this TrainerActivitiesContract trainerActivitiesContract)
        {
            var trainerActivitiesViewModel = new TrainerActivitiesViewModel();

            if (trainerActivitiesContract == null) return trainerActivitiesViewModel;

            var trainerActivityViewModel = new List<TrainerActivityViewModel>();

            if (trainerActivitiesContract.TrainerActivityContract != null)
            {
                foreach (var trainerActivityContract in trainerActivitiesContract.TrainerActivityContract)
                {
                    trainerActivityViewModel.Add(trainerActivityContract.ToViewModel());
                }
            }

            trainerActivitiesViewModel.TrainerActivityViewModels = trainerActivityViewModel;
            trainerActivitiesViewModel.LanguageTableHeader = trainerActivitiesContract.LanguageTableHeader;
            trainerActivitiesViewModel.TraineeTableHeader = trainerActivitiesContract.TraineeTableHeader;

            return trainerActivitiesViewModel;
        }
    }
}
