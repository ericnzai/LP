using System.Collections.Generic;
using LP.Model.ViewModels.Group;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.Dashboards
{
    public class GroupActivityViewModel
    {
        public int GroupTypeId { get; set; }
        //public int GroupId { get; set; }
        public string Culture { get; set; }
        public ActivitiesStatus ActivityStatus { get; set; }
    }
}
