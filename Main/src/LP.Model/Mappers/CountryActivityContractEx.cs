using LP.Model.ViewModels.Dashboards;
using LP.Model.ViewModels.Dashboards.Country;
using LP.Model.ViewModels.Dashboards.Trainer;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;

namespace LP.Model.Mappers
{
    public static class CountryActivityContractEx
    {
        public static CountryActivityViewModel ToViewModel(this CountryActivityContract countryActivityContract)
        {
            var countryActivityViewModel = new CountryActivityViewModel();
            if (countryActivityContract == null) return countryActivityViewModel;

            var traineeActivityLanguageViewModel = new List<TraineeActivityLanguageViewModel>();

            if (countryActivityContract.TraineeActivityLanguageContract != null)
            {
                foreach (var traineeActivityLanguageContract in countryActivityContract.TraineeActivityLanguageContract)
                {
                    traineeActivityLanguageViewModel.Add(traineeActivityLanguageContract.ToViewModel());
                }
            }

            countryActivityViewModel.TraineeActivityLanguageViewModels = traineeActivityLanguageViewModel;
            countryActivityViewModel.TraineeUserName = countryActivityContract.TraineeUserName;
            countryActivityViewModel.TrainerUserName = countryActivityContract.TrainerUserName;
            
            return countryActivityViewModel;
        }
    }
}
