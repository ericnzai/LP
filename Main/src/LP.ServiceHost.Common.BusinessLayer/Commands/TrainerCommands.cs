using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Views;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class TrainerCommands : ITrainerCommands
    {
        private readonly IBaseCommands _baseCommands;

        public TrainerCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<IEnumerable<TrainersWithStudentsCountries>> GetAllTrainersAsync()
        {
            var trainers = await _baseCommands.GetAllAsync<TrainersWithStudentsCountries>();
            return trainers;
        }

        public async Task<IEnumerable<TrainersWithStudentsCountries>> GetTrainersByCountryIdAsync(int countryId)
        {
            if (countryId < 0)
            {
                return null;
            }

            var trainers = await _baseCommands.GetAllAsync<TrainersWithStudentsCountries>();

            return trainers.Where(t => t.CountryID == countryId);
        }

        public async Task<IEnumerable<int>> GetTrainersIdsByCountryId(int countryId)
        {
            var trainers = await GetTrainersByCountryIdAsync(countryId);

            return trainers.Select(t => t.UserID);
        } 
    }
}
