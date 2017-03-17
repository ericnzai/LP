using System.Collections.Generic;

namespace LP.Model.ViewModels.Dashboards.Student
{
    public class TrainingAreaProgressViewModel
    {
        public TrainingAreaProgressViewModel()
        {
            GroupProgressViewModels = new List<GroupProgressViewModel>();
        }

        public string TrainingAreaName { get; set; }
        public int TrainingAreaId { get; set; }
        public List<GroupProgressViewModel> GroupProgressViewModels { get; set; } 

    }
}
