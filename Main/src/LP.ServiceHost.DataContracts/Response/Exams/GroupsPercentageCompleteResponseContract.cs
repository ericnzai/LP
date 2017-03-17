using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class GroupsPercentageCompleteResponseContract
    {
        public List<GroupPercentageComplete> GroupsPercentageComplete { get; set; }
    }
}
