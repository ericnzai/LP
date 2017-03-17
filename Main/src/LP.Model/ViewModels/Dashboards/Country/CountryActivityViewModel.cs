using System.Collections.Generic;
using LP.Model.ViewModels.Group;

namespace LP.Model.ViewModels.Dashboards.Country
{
    public class CountryActivityViewModel
    {
        public string TraineeUserName { get; set; }
        public string TrainerUserName { get; set; }
        public List<TraineeActivityLanguageViewModel> TraineeActivityLanguageViewModels { get; set; }
    }
}
