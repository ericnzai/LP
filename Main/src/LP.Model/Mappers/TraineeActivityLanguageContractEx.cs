using LP.Model.ViewModels.Dashboards;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.Model.Mappers
{
    public static class TraineeActivityLanguageContractEx
    {
        public static TraineeActivityLanguageViewModel ToViewModel(this TraineeActivityLanguageContract traineeActivityLanguageContract)
        {
            var traineeActivityLanguageViewModel = new TraineeActivityLanguageViewModel();

            if (traineeActivityLanguageContract == null) return traineeActivityLanguageViewModel;

            var groupActivityViewModel = new List<GroupActivityViewModel>();

            foreach (var groupActivityContract in traineeActivityLanguageContract.GroupActivityContract)
            {
                groupActivityViewModel.Add(groupActivityContract.ToViewModel());
            }

            traineeActivityLanguageViewModel.GroupActivityViewModels = groupActivityViewModel;
            traineeActivityLanguageViewModel.Language = traineeActivityLanguageContract.Language;
            traineeActivityLanguageViewModel.CultureCode = traineeActivityLanguageContract.CultureCode;


            return traineeActivityLanguageViewModel;
        }
    }
}
