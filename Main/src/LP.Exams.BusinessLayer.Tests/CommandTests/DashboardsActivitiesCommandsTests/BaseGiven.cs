
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.EntityModels.Views;
using LP.Exams.BusinessLayer.Commands;
using LP.Exams.BusinessLayer.Tests.Helpers;
using LP.Model.Authentication;
using LP.Model.Exams;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsActivitiesCommandsTests
{
    public class BaseGiven : SpecsFor<DashboardActivitiesCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IRoleCommands> RoleCommandsMock = new Mock<IRoleCommands>();
        protected readonly Mock<IUserCommands> UserCommandsMock = new Mock<IUserCommands>();
        protected readonly Mock<IUserRoleCommands> UserRoleCommandsMock = new Mock<IUserRoleCommands>();
        protected readonly Mock<IGroupTypeCommands> GroupTypeCommandsMock = new Mock<IGroupTypeCommands>();
        protected readonly Mock<ITrainingAreaCommands> TrainingAreaCommandsMock = new Mock<ITrainingAreaCommands>();
        protected readonly Mock<ITrainerCommands> TrainerCommandsMock = new Mock<ITrainerCommands>();
        protected readonly Mock<IUserPostViewedCommands> UserPostViewedCommandsMock = new Mock<IUserPostViewedCommands>();
        protected readonly Mock<IAttemptsCommands> AttemptsCommnadsMock = new Mock<IAttemptsCommands>();
        protected readonly Mock<IExamCommands> ExamCommandsMock = new Mock<IExamCommands>();
        protected readonly Mock<IFilterAllowedUser> FilterAllowedUserMock = new Mock<IFilterAllowedUser>();
        protected readonly Mock<ICertificatesAchievedCommands> CertificatesAchievedCommands = new Mock<ICertificatesAchievedCommands>();

        protected List<int> FilteredUsers = new List<int>
        {
            1,2
        };

      

        protected List<UserRole> UserRoles = new List<UserRole>
        {
            new UserRole{UserID = 1, RoleID = 1, askCore_Roles = new Role{RoleID = 1, RoleName = "en", RoleGroupID = 1}},
            new UserRole{UserID = 2, RoleID = 1, askCore_Roles = new Role{RoleID = 1, RoleName = "en", RoleGroupID = 1}}
        }; 
        protected  List<UserRole> JobUserRoles = new List<UserRole>
        {
            new UserRole{UserID = 3, RoleID = 2, askCore_Roles = new Role{RoleID = 2, RoleName = "MSL", RoleGroupID = 1}},
            new UserRole{UserID = 4, RoleID = 3, askCore_Roles = new Role{RoleID = 3, RoleName = "Medical", RoleGroupID = 1}}
        };
        protected List<User> JobRoleUsers = new List<User>
        {
            new User {UserID = 3,Culture = "en", askCore_UsersRoles = new List<UserRole> {  new UserRole{UserID = 3, RoleID = 2, askCore_Roles = new Role{RoleID = 2, RoleName = "MSL", RoleGroupID = 8},}} },
            new User {UserID = 4, Culture = "en", askCore_UsersRoles = new List<UserRole> { new UserRole { UserID = 4, RoleID = 3, askCore_Roles = new Role { RoleID = 3, RoleName = "Medical", RoleGroupID = 8 } } } }

        };
        protected List<Group> Groups = new List<Group>
        {
            new Group{GroupID = 1, GroupTypeID = 1},
            new Group{GroupID = 2, GroupTypeID = 1},
        }; 
        protected List<TrainingsExam> TrainingsExams = new List<TrainingsExam>
        {
            new TrainingsExam{ExamId = 1, GroupId = 1},
            new TrainingsExam{ExamId = 2, GroupId = 1},
            new TrainingsExam{ExamId = 3, GroupId = 2},
        };
        protected List<Attempt> Attempts = new List<Attempt>
        {
            new Attempt{ExamId = 1, UserId = 1, GroupId = 1},
            new Attempt{ExamId = 2, UserId = 1, GroupId = 1},
            new Attempt {ExamId = 1 ,UserId = 3,GroupId = 1},
            new Attempt {ExamId = 2 ,UserId = 3,GroupId = 1}
        };

        protected List<ltl_UserPostViewed> UserPostVieweds = new List<ltl_UserPostViewed>
        {
            new ltl_UserPostViewed{upv_GroupId = 2, upv_UserId = 2}
        };
        protected List<ltl_GroupType> GropTypes = new List<ltl_GroupType>
        {
            new ltl_GroupType{ID = 1, Name = "Group Type"}
        };
        protected List<DecryptedUser> DecryptedUsers = new List<DecryptedUser>
        {
            new DecryptedUser{UserId = 1, DecryptedDisplayName = "Existing User", DecryptedUserName = "existing@user.com"},
            new DecryptedUser{UserId = 2, DecryptedDisplayName = "Another User", DecryptedUserName = "another@user.com"},
            new DecryptedUser{UserId = 3, DecryptedDisplayName = "MSL User", DecryptedUserName = "msl@user.com"},
            new DecryptedUser{UserId = 2, DecryptedDisplayName = "Medical User", DecryptedUserName = "medical@user.com"}
        };

        protected List<CertificatesAchieved> CertificatesAchieved = new List<CertificatesAchieved>
        {
            new CertificatesAchieved{GroupId = 1, UserId = 1},
            new CertificatesAchieved{GroupId = 2, UserId = 1},
        };

        protected void PrepareSut()
        {
            FilterAllowedUserMock.Setup(m => m.GetUserIdsFilteredByTrainer(It.IsAny<int>())).ReturnsAsync(FilteredUsers.ToList());
            FilterAllowedUserMock.Setup(
                m => m.GetUsersFilteredByTrainerAndJobFunctions(It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(JobRoleUsers);

            BaseCommandsMock.Setup(m => m.GetAllAsync<Group>()).ReturnsAsync(Groups.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<TrainingsExam>()).ReturnsAsync(TrainingsExams.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetConditionalWithIncludesAsync<TrainingsExam>(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>())).ReturnsAsync(TrainingsExams.AsQueryable());

            UserRoleCommandsMock.Setup(m => m.GetCultureRolesForUsers(It.IsAny<List<int>>())).ReturnsAsync(UserRoles);
            
            AttemptsCommnadsMock.Setup(m => m.GetAttempedExamIdsGroupedByUserIdList(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(Attempts.GroupBy(a => a.UserId, a => a.ExamId).ToList());

            UserPostViewedCommandsMock.Setup(m => m.GetUserPostsViewedGroupedList(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(UserPostVieweds.GroupBy(a=>a.upv_UserId, a=>a.upv_GroupId).ToList());

            UserCommandsMock.Setup(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>())).Returns(DecryptedUsers);

            GroupTypeCommandsMock.Setup(m => m.GetAllAvailableGroupTypes()).ReturnsAsync(GropTypes);

            //var certificatesMoqDbSetProvider = new MoqDbSetProvider<CertificatesAchieved>();

            //var moqDbSet = certificatesMoqDbSetProvider.DbSet(CertificatesAchieved);

            CertificatesAchievedCommands.Setup(
                m => m.GetCertificatesAchievedForUsersAndGroups(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(CertificatesAchieved);
            
            SUT = new DashboardActivitiesCommands(BaseCommandsMock.Object, TrainerCommandsMock.Object, UserCommandsMock.Object,
                                                  UserRoleCommandsMock.Object, GroupTypeCommandsMock.Object, AttemptsCommnadsMock.Object,
                                                  UserPostViewedCommandsMock.Object, FilterAllowedUserMock.Object, CertificatesAchievedCommands.Object);
        }
    }
}
