using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Authentication.BusinessLayer.Commands;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class BaseGiven : SpecsFor<UserRoleCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<ICultureProvider> CultureProviderMock = new Mock<ICultureProvider>();
        protected List<UserRole> UserRoles = new List<UserRole>();
        protected List<askCore_RoleGroup> RoleGroups = new List<askCore_RoleGroup>();
        protected List<Role> Roles = new List<Role>();
        protected  Dictionary<string, string> CultureRolesDictionary = new Dictionary<string, string>();

        protected Expression<Func<UserRole, bool>> UserRoleExpression = null;

        protected void PrepareSut()
        {
            var moqDbSetProvider = new MoqDbSetProvider<UserRole>();
            var mockUserRoleDbSet = moqDbSetProvider.DbSet(UserRoles);

            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<UserRole, object>>[]>()))
                .ReturnsAsync(mockUserRoleDbSet.Object);
            
            BaseCommandsMock.Setup(m => m.GetConditionalAsync<UserRole>(It.IsAny<Expression<Func<UserRole, bool>>>())).ReturnsAsync(mockUserRoleDbSet.Object);

            var roleGroupMoqDbSetProvider = new MoqDbSetProvider<askCore_RoleGroup>();
            var mockRoleGroupDbSet = roleGroupMoqDbSetProvider.DbSet(RoleGroups);

            BaseCommandsMock.Setup(m => m.GetAllAsync<askCore_RoleGroup>()).ReturnsAsync(mockRoleGroupDbSet.Object);
            CultureProviderMock.Setup(m => m.GetCultureRoles()).ReturnsAsync(CultureRolesDictionary);

            var roleMoqDbSetProvider = new MoqDbSetProvider<Role>();
            var mockRoleDbSet = roleMoqDbSetProvider.DbSet(Roles);

            BaseCommandsMock.Setup(m => m.GetAllAsync<Role>()).ReturnsAsync(mockRoleDbSet.Object);
            
            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync<Role>(It.IsAny<Expression<Func<Role,bool>>>(), It.IsAny<Expression<Func<Role, object>>[]>())).ReturnsAsync(mockRoleDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync<Role>(r => r.askCore_RoleGroup.RoleGroupName == RoleGroup.CultureRoles.ToString(), It.IsAny<Expression<Func<Role, object>>[]>())).ReturnsAsync(mockRoleDbSet.Object);
            
            SUT = new UserRoleCommands(BaseCommandsMock.Object);
 
        }
    }
}
