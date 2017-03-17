using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IGroupCompletionCommands
    {
        Task<TrainingAreaProgressResponseContract> GetAllCompleteGroupsForTrainingAreaAsync(int trainingAreaId, UserDetails authenticatedUserDetails);
    }
}
