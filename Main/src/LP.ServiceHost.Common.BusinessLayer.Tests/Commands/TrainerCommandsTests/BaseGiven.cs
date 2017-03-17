using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainerCommandsTests
{
    public class BaseGiven : SpecsFor<TrainerCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected static int ExistingCountryId = 1;

        protected List<TrainersWithStudentsCountries> Trainers = new List<TrainersWithStudentsCountries>
        {
            new TrainersWithStudentsCountries {UserID = 1,DisplayName="John Smith", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 2,DisplayName="Jane Jones", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 3,DisplayName="Patrick Williams", CountryID = ExistingCountryId},
            new TrainersWithStudentsCountries {UserID = 4,DisplayName="Anna Taylor", CountryID = 2},
            new TrainersWithStudentsCountries {UserID = 5,DisplayName="Miranda Brown", CountryID = 2}, 
            new TrainersWithStudentsCountries {UserID = 6,DisplayName="Michael Davies", CountryID = 2}
        };
       
        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<TrainersWithStudentsCountries>()).ReturnsAsync(Trainers.AsQueryable());
            SUT = new TrainerCommands(BaseCommandsMock.Object);
        }
    }
}
