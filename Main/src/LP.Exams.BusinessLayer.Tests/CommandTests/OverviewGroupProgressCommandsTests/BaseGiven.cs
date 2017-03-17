using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.EntityModels;
using LP.Exams.BusinessLayer.Commands;
using LP.Model.Authentication;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewGroupProgressCommandsTests
{
    public class BaseGiven : SpecsFor<OverviewGroupTypeProgressCommands>
    {
        protected readonly Mock<ICommonCalculatorCommands> CommonCalculatorCommandsMock = new Mock<ICommonCalculatorCommands>();
        protected readonly Mock<IAttemptsCommands> AttemptCommandsMock = new Mock<IAttemptsCommands>();
        protected readonly Mock<ICertificatesAchievedCommands> CertificatesAchievedCommandsMock = new Mock<ICertificatesAchievedCommands>();
        protected readonly Mock<IUserPostViewedCommands> UserPostViewedCommandsMock = new Mock<IUserPostViewedCommands>();
        protected readonly Mock<IFilterAllowedUser> AllowedUserFilterMock = new Mock<IFilterAllowedUser>();
        protected readonly Mock<IFilterAllowedGroups> FilterAllowedGroupsMock = new Mock<IFilterAllowedGroups>();
        protected readonly Mock<IFilterCertificatesAchieved> FilterCertificatesAchievedMock = new Mock<IFilterCertificatesAchieved>();
        protected int GroupTypeId = 1;
        protected ltl_GroupType GroupType = new ltl_GroupType{Name = "Test Group Type"};
        protected UserDetails UserDetails = new UserDetails {UserId = 1};
        protected List<int> GroupTypeIds = new List<int > {1,2,3}; 

        protected static ltl_GroupType GroupType1 = new ltl_GroupType{ID= 1,Name = "GT 1"};
        protected static ltl_GroupType GroupType2 = new ltl_GroupType{ID= 2,Name = "GT 2"};
        protected static ltl_GroupType GroupType3 = new ltl_GroupType{ID= 3,Name = "GT 3"};


        protected List<ltl_GroupType> GroupTypes = new List<ltl_GroupType>
        {
            GroupType1, GroupType2, GroupType3
        }; 

        protected List<Group> Groups = new List<Group>
        {
            new Group {GroupID = 1, GroupTypeID = 1, ltl_GroupType =  GroupType1 },
            new Group {GroupID = 2, GroupTypeID = 1, ltl_GroupType =  GroupType1 },
            new Group {GroupID = 3, GroupTypeID = 1, ltl_GroupType =  GroupType1 },
            new Group {GroupID = 4, GroupTypeID = 1, ltl_GroupType =  GroupType1 },
            new Group {GroupID = 5, GroupTypeID = 1, ltl_GroupType =  GroupType1 },
            new Group {GroupID = 6, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 7, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 8, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 9, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 10, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 11, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 12, GroupTypeID = 2, ltl_GroupType =  GroupType2 },
            new Group {GroupID = 13, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 14, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 15, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 16, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 17, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 18, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 19, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 20, GroupTypeID = 3, ltl_GroupType =  GroupType3 },
            new Group {GroupID = 21, GroupTypeID = 3, ltl_GroupType =  GroupType3 }

        }; 
        protected List<int> UserIds = new List<int> {6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36}; 
        
        protected List<ltl_UserPostViewed> UserPostsViewed = new List<ltl_UserPostViewed>
        {
            new ltl_UserPostViewed{upv_UserId = 6, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 6, upv_GroupId = 2},new ltl_UserPostViewed{upv_UserId = 6, upv_GroupId = 3},
            new ltl_UserPostViewed{upv_UserId = 6, upv_GroupId = 4},new ltl_UserPostViewed{upv_UserId = 6, upv_GroupId = 5},new ltl_UserPostViewed{upv_UserId = 7, upv_GroupId = 1},
            new ltl_UserPostViewed{upv_UserId = 7, upv_GroupId = 2},new ltl_UserPostViewed{upv_UserId = 7, upv_GroupId = 3}, new ltl_UserPostViewed{upv_UserId = 7, upv_GroupId = 4},
            new ltl_UserPostViewed{upv_UserId = 7, upv_GroupId = 5}, new ltl_UserPostViewed{upv_UserId = 8, upv_GroupId = 1}, new ltl_UserPostViewed{upv_UserId = 9, upv_GroupId = 1},
            new ltl_UserPostViewed{upv_UserId = 10, upv_GroupId = 1}, new ltl_UserPostViewed{upv_UserId = 11, upv_GroupId = 1}, new ltl_UserPostViewed{upv_UserId = 12, upv_GroupId = 1},
            new ltl_UserPostViewed{upv_UserId = 13, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 14, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 15, upv_GroupId = 1},
            new ltl_UserPostViewed{upv_UserId = 16, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 17, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 18, upv_GroupId = 1},
            new ltl_UserPostViewed{upv_UserId = 19, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 20, upv_GroupId = 1},new ltl_UserPostViewed{upv_UserId = 21, upv_GroupId = 1}
        }; 
        
        protected void PrepareSut()
        {
            FilterAllowedGroupsMock.Setup(m => m.GetAllLiveGroupsList())
                .ReturnsAsync(Groups);

            AllowedUserFilterMock.Setup(m => m.GetAllLiveUsersNotHiddenFromReportsIds()).ReturnsAsync(UserIds);

            AllowedUserFilterMock.Setup(m => m.GetUserIdsFilteredByRegion(It.IsAny<int>())).ReturnsAsync(UserIds);

            UserPostViewedCommandsMock.Setup(m => m.GetUserPostsViewedGroupedByGroup(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(UserPostsViewed.GroupBy(a => a.upv_GroupId, a => a.upv_UserId).ToList());

            CommonCalculatorCommandsMock.Setup(m => m.CalculatePercentages(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int numberOfItems, int totalItems) =>
                    {
                
                    if (numberOfItems <= 0 || totalItems <= 0) return 0;

                    var percentageCalculation = ((decimal)numberOfItems / totalItems) * 100;

                    return (int)percentageCalculation;
                });

            SUT = new OverviewGroupTypeProgressCommands(
                AttemptCommandsMock.Object, 
                AllowedUserFilterMock.Object, 
                CommonCalculatorCommandsMock.Object, 
                CertificatesAchievedCommandsMock.Object,
                UserPostViewedCommandsMock.Object, FilterAllowedGroupsMock.Object);
        }
    }
}
