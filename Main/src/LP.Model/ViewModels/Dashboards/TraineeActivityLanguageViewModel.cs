using System.Collections.Generic;

namespace LP.Model.ViewModels.Dashboards
{
    public class TraineeActivityLanguageViewModel
    {
        public string Language { get; set; }
        public string CultureCode { get; set; }
        public List<GroupActivityViewModel> GroupActivityViewModels { get; set; }
    }

}
