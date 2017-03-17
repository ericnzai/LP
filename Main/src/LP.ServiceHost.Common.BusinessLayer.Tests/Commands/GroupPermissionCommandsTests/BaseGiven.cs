using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupPermissionCommandsTests
{
    public class BaseGiven : SpecsFor<GroupPermissionCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        
        protected List<GroupPermission> GroupPermissions = new List<GroupPermission>
        {
            new GroupPermission{GroupID = 1, ltl_Groups = new Group{GroupID = 1,StatusBankID = 1, TrainingAreaID = 1}, RoleID = 1},
            new GroupPermission{GroupID = 2, ltl_Groups = new Group{GroupID = 2, StatusBankID = 2, TrainingAreaID = 1}, RoleID = 2},
            new GroupPermission{GroupID = 3, ltl_Groups = new Group{GroupID = 3, StatusBankID = 3, TrainingAreaID = 1}, RoleID = 3},
            new GroupPermission{GroupID = 4, ltl_Groups = new Group{GroupID = 4, StatusBankID = 4, TrainingAreaID = 1}, RoleID = 4},
            new GroupPermission{GroupID = 5, ltl_Groups = new Group{GroupID = 5, StatusBankID = 5, TrainingAreaID = 1}, RoleID = 5},
            new GroupPermission{GroupID = 6, ltl_Groups = new Group{GroupID = 6, StatusBankID = 6, TrainingAreaID = 1}, RoleID = 6},
            new GroupPermission{GroupID = 7, ltl_Groups = new Group{GroupID = 7, StatusBankID = 4, TrainingAreaID = 1}, RoleID = 1},
            new GroupPermission{GroupID = 8, ltl_Groups = new Group{GroupID = 8, StatusBankID = 5, TrainingAreaID = 1}, RoleID = 2},
        }; 

        protected readonly List<int> RoleIds = new List<int>{1,2,3};
        protected readonly List<int> AvailableStatuses = new List<int> { 4, 5, 6 };
        
        protected void PrepareSut()
        {
            var groupPermissionMoqDbSetProvider = new MoqDbSetProvider<GroupPermission>();

            var moqGroupPermissionDbSet = groupPermissionMoqDbSetProvider.DbSet(GroupPermissions);

            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<GroupPermission, object>>[]>()))
                .ReturnsAsync(moqGroupPermissionDbSet.Object);
          
            SUT = new GroupPermissionCommands(BaseCommandsMock.Object);
        }
    }
}
