using System.Collections.Generic;
using LP.Model.ViewModels.Group;

namespace LP.Model.ViewModels.Dashboards.Trainer
{
    public class TrainerActivityViewModel
    {
        public string TraineeUserName { get; set; }
        public List<TraineeActivityLanguageViewModel> TraineeActivityLanguageViewModels { get; set; }
    }
}
