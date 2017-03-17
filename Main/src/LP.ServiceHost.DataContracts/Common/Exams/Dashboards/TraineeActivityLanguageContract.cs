using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class TraineeActivityLanguageContract
    {
        public string Language { get; set; }
        public string CultureCode { get; set; }
        public List<GroupActivityContract> GroupActivityContract { get; set; }
    }
}
