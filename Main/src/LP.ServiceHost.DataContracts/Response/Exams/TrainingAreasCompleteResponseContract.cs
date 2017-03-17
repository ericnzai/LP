using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.ServiceHost.DataContracts.Response.Exams
{
    public class TrainingAreasCompleteResponseContract
    {
        public TrainingAreasCompleteResponseContract()
        {
            TrainingAreaCompletions = new List<TrainingAreaCompletion>();
        }

        public List<TrainingAreaCompletion> TrainingAreaCompletions { get; set; }
    }
}
