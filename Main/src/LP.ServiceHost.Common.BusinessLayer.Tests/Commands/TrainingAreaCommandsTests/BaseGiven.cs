using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainingAreaCommandsTests
{
    public class BaseGiven : SpecsFor<TrainingAreaCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<ITrainingGroupPermissionFilter> TrainingGroupPermissionFilterMock = new Mock<ITrainingGroupPermissionFilter>();
        
        protected List<TrainingArea> TrainingAreas = new List<TrainingArea>
        {
            new TrainingArea {TrainingAreaID = 1, StatusBankID = 1},
            new TrainingArea {TrainingAreaID = 2, StatusBankID = 2},
            new TrainingArea {TrainingAreaID = 3, StatusBankID = 1},
            new TrainingArea {TrainingAreaID = 4, StatusBankID = 2},
            new TrainingArea {TrainingAreaID = 5, StatusBankID = 1}, 
            new TrainingArea {TrainingAreaID = 6, StatusBankID = 2}
        };
        
        protected List<Group> Groups = new List<Group>
        {
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 1
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 1
            },
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 1
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 2
            },
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 1
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 3
            },
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 2
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 4
            },
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 2
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 5
            },
            new Group
            {
                ltl_GroupPermissions = new Collection<GroupPermission>(), 
                TrainingArea = new TrainingArea
                {
                    TrainingAreaPermissions = new Collection<ltl_TrainingAreaPermissions>(),
                    TrainingAreaID = 2
                }, 
                ltl_Sections = new Collection<ltl_Sections>(),
                GroupID = 6
            }
        }; 

        protected void PrepareSut()
        {

            BaseCommandsMock.Setup(m => m.GetAllAsync<TrainingArea>()).ReturnsAsync(TrainingAreas.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync<Group>(It.IsAny<Expression<Func<Group, object>>[]>())).ReturnsAsync(Groups.AsQueryable());
            //BaseCommandsMock.Setup(m => m.GetAll<Group>()).Returns(Groups.AsQueryable());

            TrainingGroupPermissionFilterMock.Setup(m => m.GetAvailableByStatusOfAsync<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()))
                .Returns((IQueryable<Group> groups, IEnumerable<int> roleIds, AccessType accessType) => Task.FromResult(groups));

            TrainingGroupPermissionFilterMock.Setup(m => m.GetAvailableByPermissionsOf<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                    It.IsAny<IEnumerable<int>>()))
                .Returns((IQueryable<Group> groups, IEnumerable<int> roles) =>  groups);

            TrainingGroupPermissionFilterMock.Setup(m => m.GetAvailableByStatusOfAsync<Group>(It.IsAny<IQueryable<Group>>(),
                       It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()))
               .Returns((IQueryable<Group> groups, IEnumerable<int> roleIds, AccessType accessType) => Task.FromResult(groups));

            TrainingGroupPermissionFilterMock.Setup(m => m.GetAvailableByPermissionsOf<Group>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>()))
               .Returns((IQueryable<Group> groups, IEnumerable<int> roles) => groups);

            SUT = new TrainingAreaCommands(BaseCommandsMock.Object, TrainingGroupPermissionFilterMock.Object);
        }
    }
}
