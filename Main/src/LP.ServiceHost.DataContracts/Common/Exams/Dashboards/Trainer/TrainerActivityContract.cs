using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer
{
    public class TrainerActivityContract
    {
        public string TraineeUserName { get; set; }
        public List<TraineeActivityLanguageContract> TraineeActivityLanguageContract { get; set; }
    }
}
