using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels.StoredProcedure.Input;
using LP.EntityModels.StoredProcedure.Output;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.SearchCommandsTests
{
    public class BaseGiven : SpecsFor<SearchCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected List<SearchWithRowCount> SearchItems = new List<SearchWithRowCount>();
        
        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<SearchWithRowCount>()).ReturnsAsync(SearchItems.AsQueryable());

            BaseCommandsMock.Setup(
                m =>
                    m.ExecuteStoredProcedure<SearchWithRowCount, ltl_SearchWithRowCountArguments>(It.IsAny<ltl_SearchWithRowCountArguments>()))
                .Returns(SearchItems.AsEnumerable());


            SUT = new SearchCommands(BaseCommandsMock.Object);
        }
    }
}
