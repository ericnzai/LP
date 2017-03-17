using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class GroupActivityContract
    {
        public int GroupTypeId { get; set; }
        public string Culture { get; set; }
        public ActivitiesStatus ActivityStatus { get; set; }
    }
}
