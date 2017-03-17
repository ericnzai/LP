using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country
{
    public class CountryActivityContract
    {
        public string TraineeUserName { get; set; }
        public string TrainerUserName { get; set; }
        public List<TraineeActivityLanguageContract> TraineeActivityLanguageContract { get; set; }
    }
}
