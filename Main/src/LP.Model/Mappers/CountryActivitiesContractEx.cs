using LP.Model.ViewModels.Dashboards.Country;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country;

namespace LP.Model.Mappers
{
    public static class CountryActivitiesContractEx
    {
        public static CountryActivitiesViewModel ToViewModel(this CountryActivitiesContract countryActivitiesContract)
        {
            var countryActivitiesViewModel = new CountryActivitiesViewModel();

            if (countryActivitiesContract == null) return countryActivitiesViewModel;

            var countryActivityViewModel = new List<CountryActivityViewModel>();

            if (countryActivitiesContract.CountryActivityContract != null)
            {
                foreach (var countryActivityContract in countryActivitiesContract.CountryActivityContract)
                {
                    countryActivityViewModel.Add(countryActivityContract.ToViewModel());
                }
            }

            countryActivitiesViewModel.CountryActivityViewModels = countryActivityViewModel;
            countryActivitiesViewModel.LanguageTableHeader = countryActivitiesContract.LanguageTableHeader;
            countryActivitiesViewModel.TraineeTableHeader = countryActivitiesContract.TraineeTableHeader;
            countryActivitiesViewModel.TrainerTableHeader = countryActivitiesContract.TrainerTableHeader;

            return countryActivitiesViewModel;
        }
    }
}
