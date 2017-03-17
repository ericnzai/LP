using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupCommandsTests
{
    public class BaseGiven : SpecsFor<GroupCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<Group> Groups = new List<Group>
        {
            new Group {GroupID = 1, StatusBankID = 1},
            new Group {GroupID = 2, StatusBankID = 2},
            new Group {GroupID = 3, StatusBankID = 1},
            new Group {GroupID = 4, StatusBankID = 2},
            new Group {GroupID = 5, StatusBankID = 1}, 
            new Group {GroupID = 6, StatusBankID = 2}
        };

        protected void PrepareSut()
        {
            var groupMoqDbSetProvider = new MoqDbSetProvider<Group>();

            var moqDbSet = groupMoqDbSetProvider.DbSet(Groups);

            BaseCommandsMock.Setup(m => m.GetAllAsync<Group>()).ReturnsAsync(moqDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetConditionalAsync<Group>(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(moqDbSet.Object);

            SUT = new GroupCommands(BaseCommandsMock.Object);
        }
    }
}
