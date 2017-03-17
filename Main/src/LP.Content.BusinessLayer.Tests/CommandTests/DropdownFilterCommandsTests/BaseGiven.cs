using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.Model.Authentication;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.DropdownFilterCommandsTests
{
    public class BaseGiven : SpecsFor<DropdownFilterCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IRoleProvider> RoleProviderMock = new Mock<IRoleProvider>();
        protected readonly Mock<ITrainerCommands> TrainerCommandsMock = new Mock<ITrainerCommands>();
        protected readonly Mock<IUserCommands> UserCommandsMock = new Mock<IUserCommands>();
        protected List<Country> Countries = new List<Country>
        {
            new Country{ CountryID = 1, RegionId = 1, CountryName = "Country 1"},
            new Country{ CountryID = 2, RegionId = 1, CountryName = "Country 2"},
            new Country{ CountryID = 3, RegionId = 1, CountryName = "Country 3"},
            new Country{ CountryID = 4, RegionId = 1, CountryName = "Country 4"},
            new Country{ CountryID = 5, RegionId = 1, CountryName = "Country 5"},
            new Country{ CountryID = 6, RegionId = 1, CountryName = "Country 6"},
            new Country{ CountryID = 7, RegionId = 2, CountryName = "Country 7"},
            new Country{ CountryID = 8, RegionId = 2, CountryName = "Country 8"},
            new Country{ CountryID = 9, RegionId = 2, CountryName = "Country 9"},
            new Country{ CountryID = 10, RegionId = 2, CountryName = "Country 10"},
            new Country{ CountryID = 11, RegionId = 3, CountryName = "Country 11"},
            new Country{ CountryID = 12, RegionId = 3, CountryName = "Country 12"},
            new Country{ CountryID = 13, RegionId = 3, CountryName = "Country 13"}
        };

        protected List<int> TrainersWithStudentsCountries = new List<int>
        {
            17, 12, 23, 82, 47, 65
        };

        protected List<Role> Roles = new List<Role>
        {
            new Role{RoleID = 1, RoleName = "Role 1"},
            new Role{RoleID = 2, RoleName = "Role 2"},
            new Role{RoleID = 3, RoleName = "Role 3"},
            new Role{RoleID = 4, RoleName = "Role 4"},
            new Role{RoleID = 5, RoleName = "Role 5"},
            new Role{RoleID = 6, RoleName = "Role 6"},
            new Role{RoleID = 7, RoleName = "Role 7"},
        };
 
        protected List<Region> Regions = new List<Region>
        {
            new Region{RegionId = 1, Name = "Europe"},
            new Region{RegionId = 2, Name = "Africa"},
            new Region{RegionId = 3, Name = "Canada"},
            new Region{RegionId = 4, Name = "USA"},
            new Region{RegionId = 5, Name = "Australia"},
            new Region{RegionId = 6, Name = "China"},
            new Region{RegionId = 7, Name = "Asia"},
            new Region{RegionId = 8, Name = "Antarctica"},
        };

        protected List<DecryptedUser> DecryptedUsers = new List<DecryptedUser>
        {
            new DecryptedUser{UserId = 17, DecryptedDisplayName = "Display name 17"},
            new DecryptedUser{UserId = 12, DecryptedDisplayName = "Display name 12"},
            new DecryptedUser{UserId = 23, DecryptedDisplayName = "Display name 23"},
            new DecryptedUser{UserId = 82, DecryptedDisplayName = "Display name 82"},
            new DecryptedUser{UserId = 47, DecryptedDisplayName = "Display name 47"},
            new DecryptedUser{UserId = 65, DecryptedDisplayName = "Display name 65"},
        };

        protected void PrepareSut()
        {
            var countryMoqDbSet = new MoqDbSetProvider<Country>().DbSet(Countries);

            BaseCommandsMock.Setup(m => m.GetConditionalAsync<Country>(It.IsAny<Expression<Func<Country, bool>>>()))
                .ReturnsAsync(countryMoqDbSet.Object);

            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync<Country>(It.IsAny<Expression<Func<Country, bool>>>(), It.IsAny<Expression<Func<Country, Object>>>()))
                .ReturnsAsync(Countries.AsQueryable());

            BaseCommandsMock.Setup(m => m.GetAllAsync<Country>())
                .ReturnsAsync(countryMoqDbSet.Object);

            TrainerCommandsMock.Setup(m => m.GetTrainersIdsByCountryId(It.Is<int>(x => x == 1))).ReturnsAsync(TrainersWithStudentsCountries);

            UserCommandsMock.Setup(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>())).Returns(DecryptedUsers);

            BaseCommandsMock.Setup(m => m.GetAllAsync<Region>()).ReturnsAsync(Regions.AsQueryable());

            BaseCommandsMock.Setup(m => m.GetConditionalAsync<Region>(It.IsAny<Expression<Func<Region, bool>>>()))
                .ReturnsAsync(Regions.AsQueryable());

            var rolesMoqDbSet = new MoqDbSetProvider<Role>().DbSet(Roles);

            RoleProviderMock.Setup(m => m.GetUserJobFunctionRolesAsync()).ReturnsAsync(rolesMoqDbSet.Object);

            SUT = new DropdownFilterCommands(BaseCommandsMock.Object, RoleProviderMock.Object, TrainerCommandsMock.Object, UserCommandsMock.Object);
        }
    }
}
