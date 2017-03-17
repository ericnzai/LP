using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
     public interface IOverviewGroupTypeProgressCommands
     {
         Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract();

         Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract(int regionId,
             int countryId, int jobRoleId, int trainerId);

         Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract(int trainerId);
         //Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeAndRegionProgressResponseContract(int regionId);
     }
}
