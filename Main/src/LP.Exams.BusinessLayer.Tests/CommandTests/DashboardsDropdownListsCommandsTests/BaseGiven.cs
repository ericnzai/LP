
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.Exams.BusinessLayer.Commands;
using LP.Model.Authentication;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsDropdownListsCommandsTests
{
    public class BaseGiven : SpecsFor<DashboardDropdownListsCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IRoleCommands> RoleCommandsMock = new Mock<IRoleCommands>();
        protected readonly Mock<ITrainerCommands> TrainerCommandsMock = new Mock<ITrainerCommands>();

        protected static int ExistingUserId = 1;
        protected static int CountryWithNoTrainersId = 6;
        protected IEnumerable<User> Users = new List<User>
        {
            new User{UserID = ExistingUserId, CountryID = ExistingCountryId, ParentID = 10},
            new User{UserID = 2, CountryID = ExistingCountryId, ParentID = 10},
            new User{UserID = 3, CountryID = ExistingCountryId, ParentID = 10},
            new User{UserID = 4, CountryID = 2, ParentID = 40},
            new User{UserID = 5, CountryID = CountryWithNoTrainersId, ParentID = 40},
        };

        protected List<Role> Roles = new List<Role>
        {
            new Role {RoleID = 201, RoleName = "Trainer", RoleGroupID = 3},
            new Role {RoleID = 202, RoleName = "MSL", RoleGroupID = 8},
            new Role {RoleID = 203, RoleName = "MSL", RoleGroupID = 8},
            new Role {RoleID = 204, RoleName = "Commercial", RoleGroupID = 8},
            new Role {RoleID = 205, RoleName = "Sales", RoleGroupID = 8},
            new Role {RoleID = 206, RoleName = "Other", RoleGroupID = 8},
        };

        protected static int ExistingCountryId = 1;

        protected List<TrainersWithStudentsCountries> Trainers = new List<TrainersWithStudentsCountries>
        {
            new TrainersWithStudentsCountries {UserID = 10,DisplayName="John Smith", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 20,DisplayName="Jane Jones", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 30,DisplayName="Patrick Williams", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 40,DisplayName="Anna Taylor", CountryID = 2},
            new TrainersWithStudentsCountries {UserID = 50,DisplayName="Miranda Brown", CountryID = 2}, 
            new TrainersWithStudentsCountries {UserID = 60,DisplayName="Michael Davies", CountryID = 2}
        };

        protected static int ExistingRegionId = 1;

        protected List<Region> Regions = new List<Region>
        {
            new Region
            {
                RegionId = ExistingRegionId,
                Name = "Europe"
            },
            new Region
            {
                RegionId = 2,
                Name = "Latin America"
            },
            new Region
            {
                RegionId = 3,
                Name = "Asia"
            }
        };

        protected List<Country> Countries = new List<Country>
        {
            new Country
            {
                CountryID = ExistingCountryId,
                CountryName = "Italy",
                RegionId = ExistingRegionId,
                Region = "Europe",
                IsFakeCountry = false
            },
             new Country
            {
                CountryID = 2,
                CountryName = "Germany",
                RegionId = ExistingRegionId,
                Region = "Europe",
                IsFakeCountry = false
            },
             new Country
            {
                CountryID = 3,
                CountryName = "Poland",
                RegionId = ExistingRegionId,
                Region = "Europe",
                IsFakeCountry = false
            },
             new Country 
            {
                CountryID = 4,
                CountryName = "England",
                RegionId = ExistingRegionId,
                Region = "Europe",
                IsFakeCountry = false
            },
             new Country 
            {
                CountryID = 5,
                CountryName = "Europe",
                RegionId = ExistingRegionId,
                Region = "Europe",
                IsFakeCountry = true
            },
             new Country 
            {
                CountryID = 6,
                CountryName = "Brazil",
                RegionId = 2,
                Region = "Latin America",
                IsFakeCountry = false
            }
        };

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<Role>()).ReturnsAsync(Roles.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<Country>()).ReturnsAsync(Countries.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<Region>()).ReturnsAsync(Regions.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<User>()).ReturnsAsync(Users.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetByIdAsync<User>(It.Is<int>(x => x == ExistingUserId))).ReturnsAsync(Users.FirstOrDefault(x => x.UserID == ExistingUserId));
            BaseCommandsMock.Setup(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 5))).ReturnsAsync(Users.FirstOrDefault(x => x.UserID == 5));
            BaseCommandsMock.Setup(m => m.GetByIdAsync<Country>(It.Is<int>(x => x == ExistingCountryId))).ReturnsAsync(Countries.FirstOrDefault(x => x.CountryID == ExistingCountryId));
            BaseCommandsMock.Setup(m => m.GetByIdAsync<Country>(It.Is<int>(x => x == CountryWithNoTrainersId))).ReturnsAsync(Countries.FirstOrDefault(x => x.CountryID == CountryWithNoTrainersId));
            TrainerCommandsMock.Setup(m => m.GetAllTrainersAsync()).ReturnsAsync(Trainers);
            TrainerCommandsMock.Setup(m => m.GetTrainersByCountryIdAsync(It.IsAny<int>())).Returns(
                (int? countryId) => Task.FromResult( Trainers.Where(a => a.CountryID == countryId).AsEnumerable()));

            SUT = new DashboardDropdownListsCommands(BaseCommandsMock.Object, TrainerCommandsMock.Object);
        }
    }
}
