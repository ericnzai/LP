using System.Collections.Generic;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Authentication.BusinessLayer.Commands;
using LP.EntityModels;
using Moq;
using SpecsFor;

namespace LP.Authentication.BusinessLayer.Tests.Commands.RoleCommandsTests
{
    public class BaseGiven : SpecsFor<RoleCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        
        protected List<Role> Roles = new List<Role>
        {
            new Role {RoleID = 1, RoleName = "MSL" },
            new Role {RoleID = 2, RoleName = "Medical"},
            new Role {RoleID = 3, RoleName = "Commercial"},
            new Role {RoleID = 4, RoleName = "Sales"},
            new Role {RoleID = 5, RoleName = "Other"}
        };

        protected void PrepareSut()
        {
            var roleMoqDbSetProvider = new MoqDbSetProvider<Role>();

            var roleMoqDbSet = roleMoqDbSetProvider.DbSet(Roles);

            BaseCommandsMock.Setup(m => m.GetAllAsync<Role>()).ReturnsAsync(roleMoqDbSet.Object);

            SUT = new RoleCommands(BaseCommandsMock.Object);
        }
    }
}
