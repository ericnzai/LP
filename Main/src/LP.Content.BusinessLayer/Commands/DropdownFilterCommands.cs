using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class DropdownFilterCommands : IDropdownFilterCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IRoleProvider _roleProvider;
        private readonly ITrainerCommands _trainerCommands;
        private readonly IUserCommands _userCommands;
        
        public DropdownFilterCommands(IBaseCommands baseCommands, IRoleProvider roleProvider, ITrainerCommands trainerCommands, IUserCommands userCommands)
        {
            _baseCommands = baseCommands;
            _roleProvider = roleProvider;
            _trainerCommands = trainerCommands;
            _userCommands = userCommands;
        }

        public async Task<DashboardFilterDropdownResponseContract> Region()
        {
            var countries = await
                _baseCommands.GetConditionalWithIncludesAsync<Country>(
                    c => c.Users.Select(u => u.UserStatusID == 1).Any(), inc=>inc.Users);

            var regionIds = countries.Select(c => c.RegionId);

            var regions = await _baseCommands.GetConditionalAsync<Region>(r => regionIds.Contains(r.RegionId));
            
            var dropdownItemContractsAsEnumerable = regions.Select(a => new DropdownItemContract { Value = a.RegionId, Text = a.Name });

            var initialDropdownItemContract = GetInitialDropdownItemContract("All Regions");

            var dropdownItemContracts = new List<DropdownItemContract> { initialDropdownItemContract };

            dropdownItemContracts.AddRange(dropdownItemContractsAsEnumerable);

            var dashboardFilterDropdownResponseContract = new DashboardFilterDropdownResponseContract
            {
                DropdownItemContracts = dropdownItemContracts
            };
            return dashboardFilterDropdownResponseContract;
        }

        public async Task<DashboardFilterDropdownResponseContract> Country()
        {
            var countries = await _baseCommands.GetAllAsync<Country>();

            var orderedCountries = countries.OrderByDescending(c => c.IsFakeCountry).ThenBy(c => c.CountryName);

            return await GetCountryDropDown(orderedCountries);
        }

        public async Task<DashboardFilterDropdownResponseContract> Country(int regionId)
        {
            var countries = await _baseCommands.GetAllAsync<Country>();
            if (regionId > 0)
            {
                 countries = countries.Where(x => x.RegionId == regionId);
            }

            var orderedCountries = countries.OrderByDescending(c => c.IsFakeCountry).ThenBy(c => c.CountryName);

            return await GetCountryDropDown(orderedCountries);
        }

        private static async Task<DashboardFilterDropdownResponseContract> GetCountryDropDown(IQueryable<Country> countries)
        {
            var initialDropdownItemContract = GetInitialDropdownItemContract("All Countries");

            var dropdownItemContractsAsQueryable = countries.Select(a => new DropdownItemContract { Value = a.CountryID, Text = a.IsFakeCountry ? "(Pseudo Region) " + a.CountryName : a.CountryName });

            var dropdownItemContracts = new List<DropdownItemContract> { initialDropdownItemContract };

            dropdownItemContracts.AddRange(await dropdownItemContractsAsQueryable.ToListAsync());

            var dashboardFilterDropdownResponseContract = new DashboardFilterDropdownResponseContract
            {
                DropdownItemContracts = dropdownItemContracts
            };

            return dashboardFilterDropdownResponseContract;
        }

        public async Task<DashboardFilterDropdownResponseContract> JobRole()
        {
            var roles = await _roleProvider.GetUserJobFunctionRolesAsync();

            var dropdownItemContractsAsQueryable = roles.Select(a => new DropdownItemContract { Value = a.RoleID, Text = a.RoleName });
            
            var initialDropdownItemContract = GetInitialDropdownItemContract("All Job Functions");

            var dropdownItemContracts = new List<DropdownItemContract> { initialDropdownItemContract };

            dropdownItemContracts.AddRange(await dropdownItemContractsAsQueryable.ToListAsync());

            var dashboardFilterDropdownResponseContract = new DashboardFilterDropdownResponseContract
            {
                DropdownItemContracts = dropdownItemContracts
            };

            return dashboardFilterDropdownResponseContract;
        }

        public async Task<DashboardFilterDropdownResponseContract> Trainer(int countryId)
        {
            var trainerIds = await _trainerCommands.GetTrainersIdsByCountryId(countryId);

            var decryptedTrainers = _userCommands.GetDecryptedUsers(trainerIds);

            var dropdownItemContractsAsEnumerable = decryptedTrainers.Select(a => new DropdownItemContract { Value = a.UserId, Text = a.DecryptedDisplayName });

            var initialDropdownItemContract = GetInitialDropdownItemContract("All Trainers");

            var dropdownItemContracts = new List<DropdownItemContract> { initialDropdownItemContract };

            dropdownItemContracts.AddRange(dropdownItemContractsAsEnumerable);

            var dashboardFilterDropdownResponseContract = new DashboardFilterDropdownResponseContract
            {
                DropdownItemContracts = dropdownItemContracts
            };

            return dashboardFilterDropdownResponseContract;
        }

        private static DropdownItemContract GetInitialDropdownItemContract(string defaultValue)
        {
            return new DropdownItemContract {Value = 0, Text = defaultValue};
        }
    }
}
