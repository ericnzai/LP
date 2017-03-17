using LP.Model.ViewModels.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;

namespace LP.Model.Mappers
{
    public static class GroupActivityContractEx
    {
        public static GroupActivityViewModel ToViewModel(this GroupActivityContract groupActivityContract)
        {
            return new GroupActivityViewModel
            {
                GroupTypeId = groupActivityContract.GroupTypeId,
                Culture = groupActivityContract.Culture,
                ActivityStatus = groupActivityContract.ActivityStatus
            };
        }
    }
}
