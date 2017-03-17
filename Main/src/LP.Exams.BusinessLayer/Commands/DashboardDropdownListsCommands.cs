using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.Model.Authentication;
using LP.Model.ViewModels.Dashboards.Country;
using LP.Model.ViewModels.Dashboards.Global;
using LP.Model.ViewModels.Dashboards.Regional;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.BusinessLayer.Commands
{
    public class DashboardDropdownListsCommands : IDashboardDropdownListsCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly ITrainerCommands _traininerCommands;

        public DashboardDropdownListsCommands(IBaseCommands baseCommands, ITrainerCommands traininerCommands)
        {
            _baseCommands = baseCommands;
            _traininerCommands = traininerCommands;
        }

        public async Task<CountryDropdownListsViewModel> GetCountryDropdownLists(UserDetails userDetails)
        {
            var countryId = 0;
            if (userDetails != null)
            {
                var user = await _baseCommands.GetByIdAsync<User>(userDetails.UserId);
                if (user.CountryID != null) countryId = (int)user.CountryID; 
            }
            
            var roles = await _baseCommands.GetAllAsync<Role>();
            var functions =
                roles.Where(
                    r =>
                        r.RoleName == "MSL" || r.RoleName == "Medical" || r.RoleName == "Commercial" ||
                        r.RoleName == "Sales" || r.RoleName == "Other");

            var functionsDropdown = new DropdownViewModel
            {
                DropdownItems = functions.Select(f => new DropdownItemViewModel()
                {
                    Id = f.RoleID.ToString(),
                    Name = f.RoleName
                }).ToList()
            };

            var trainers = countryId == 0
                ? await _traininerCommands.GetAllTrainersAsync() 
                : await _traininerCommands.GetTrainersByCountryIdAsync(countryId);
            
            var trainersDropdown = new DropdownViewModel
            {
                DropdownItems = trainers.Select(t => new DropdownItemViewModel()
                {
                    Id = t.UserID.ToString(),
                    Name = t.DisplayName
                }).ToList()
            };
            
            var countryDropdownListsViewModel = new CountryDropdownListsViewModel
            {
                UserFunctionList = functionsDropdown,
                TrainerList = trainersDropdown

            };
            return countryDropdownListsViewModel;
        }

        public async Task<RegionalDropdownListsViewModel> GetRegionalDropdownLists(UserDetails userDetails)
        {
            var regionId = 0;
            var region = new Country();
            if (userDetails != null)
            {
                var user = await _baseCommands.GetByIdAsync<User>(userDetails.UserId);
                if (user.CountryID != null) region = await _baseCommands.GetByIdAsync<Country>((int)user.CountryID);
                if (region.RegionId != null) regionId = (int)region.RegionId;
            }
            var roles = await _baseCommands.GetAllAsync<Role>();
            var functions =
                roles.Where(
                    r =>
                        r.RoleName == "MSL" || r.RoleName == "Medical" || r.RoleName == "Commercial" ||
                        r.RoleName == "Sales" || r.RoleName == "Other");

            var functionsDropdown = new DropdownViewModel
            {
                DropdownItems = functions.Select(f => new DropdownItemViewModel()
                {
                    Id = f.RoleID.ToString(),
                    Name = f.RoleName
                }).ToList()
            };

            var countries = await _baseCommands.GetAllAsync<Country>();
            
            if (regionId != 0)
            {
                countries = countries.Where(c => c.RegionId == regionId);
            }

            foreach (var country in countries.Where(country => country.IsFakeCountry))
            {
                country.CountryName = "(Pseudo Region)" + country.CountryName;
            }

            countries = countries.OrderByDescending(c => c.IsFakeCountry).ThenBy(c => c.CountryName);


            var countryDropdown = new DropdownViewModel()
            {
                DropdownItems = countries.Select(c => new DropdownItemViewModel()
                {
                    Id = c.CountryID.ToString(),
                    Name = c.CountryName
                }).ToList()
            };
            return new RegionalDropdownListsViewModel()
            {
                CountryList = countryDropdown,
                UserFunctionList = functionsDropdown
            };
        }

        public async Task<GlobalDropdownListsViewModel> GetGlobalDropdownLists()
        {
            var roles = await _baseCommands.GetAllAsync<Role>();
            var functions =
                roles.Where(
                    r =>
                        r.RoleName == "MSL" || r.RoleName == "Medical" || r.RoleName == "Commercial" ||
                        r.RoleName == "Sales" || r.RoleName == "Other");

            var functionsDropdown = new DropdownViewModel
            {
                DropdownItems = functions.Select(f => new DropdownItemViewModel()
                {
                    Id = f.RoleID.ToString(),
                    Name = f.RoleName
                }).ToList()
            };

            var countries = await _baseCommands.GetAllAsync<Country>();

            foreach (var country in countries.Where(country => country.IsFakeCountry))
            {
                country.CountryName = "(Pseudo Region)" + country.CountryName;
            }

            countries = countries.OrderByDescending(c => c.IsFakeCountry).ThenBy(c => c.CountryName);

            var countryDropdown = new DropdownViewModel()
            {
                DropdownItems = countries.Select(c => new DropdownItemViewModel()
                {
                    Id = c.CountryID.ToString(),
                    Name = c.CountryName
                }).ToList()
            };

            var regions = await _baseCommands.GetAllAsync<Region>();
            var regionDropDown = new DropdownViewModel()
            {
                DropdownItems = regions.Select(r => new DropdownItemViewModel()
                {
                    Id = r.RegionId.ToString(),
                    Name = r.Name
                }).ToList()
            };
            return new GlobalDropdownListsViewModel()
            {
                RegionList = regionDropDown,
                CountryList = countryDropdown,
                UserFunctionList = functionsDropdown
            };
        }
    }
}
