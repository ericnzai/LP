using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class TrainingAreaProgressResponseContract
    {
        public TrainingAreaProgressResponseContract()
        {
            GroupProgressContracts = new List<GroupProgressContract>();
        }

        public List<GroupProgressContract> GroupProgressContracts { get; set; }
        public string TrainingAreaName { get; set; }
        public int TrainingAreaId { get; set; }
    }
}
