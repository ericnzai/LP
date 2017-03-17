using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface ITrainerCommands
    {
        Task<IEnumerable<TrainersWithStudentsCountries>> GetAllTrainersAsync();
        Task<IEnumerable<TrainersWithStudentsCountries>> GetTrainersByCountryIdAsync(int countryId);
        Task<IEnumerable<int>> GetTrainersIdsByCountryId(int countryId);
    }
}
