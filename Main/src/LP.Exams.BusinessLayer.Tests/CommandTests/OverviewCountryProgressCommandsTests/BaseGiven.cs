using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.Exams.BusinessLayer.Commands;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;
using LP.EntityModels.Exam;
using Group = LP.EntityModels.Group;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewCountryProgressCommandsTests
{
    public class BaseGiven : SpecsFor<OverviewCountryProgressCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IFilterAllowedUser> AllowedUserFilterMock = new Mock<IFilterAllowedUser>();
        protected readonly Mock<ICertificatesAchievedCommands> CertificatesAchievedCommandsMock = new Mock<ICertificatesAchievedCommands>();
        protected readonly Mock<IGroupTypeCommands> GroupTypeCommandsMock = new Mock<IGroupTypeCommands>();
        protected readonly Mock<IRoleProvider> RoleProviderMock = new Mock<IRoleProvider>();
        protected readonly Mock<IFilterAllowedGroups> FilterAllowedGroupsMock = new Mock<IFilterAllowedGroups>();

        protected static int StatusLive = (int) Status.Live;

        //GroupType
        protected static int GroupTypeOneId = 1;
        protected static int GroupTypeTwoId = 2;
        protected static int GroupTypeThreeId = 3;

        protected static ltl_GroupType GroupType1 = new ltl_GroupType
        {
            ID = GroupTypeOneId,
            Name = "GT 1",
            
        };
        protected static ltl_GroupType GroupType2 = new ltl_GroupType
        {
            ID = GroupTypeTwoId,
            Name = "GT 2",
            
        };
        protected static ltl_GroupType GroupType3 = new ltl_GroupType
        {
            ID = GroupTypeThreeId,
            Name = "GT 3",

        };
        protected List<ltl_GroupType> GroupTypes = new List<ltl_GroupType>
        {
            GroupType1, GroupType2, GroupType3
        };

        //Group
        protected static int GroupWithTypeOneAndCultureEnId = 1;
        protected static int GroupWithTypeOneAndCulturePtId = 2;
        protected static int GroupWithTypeOneAndCultureEsId = 3;

        protected static Group GroupWithTypeOneAndCultureEn = new Group { GroupID = GroupWithTypeOneAndCultureEnId, GroupTypeID = GroupType1.ID, Culture = "en", Name = "G 1 EN", ltl_GroupType = GroupType1, StatusBankID = StatusLive };
        protected static Group GroupWithTypeOneAndCulturePt = new Group { GroupID = GroupWithTypeOneAndCulturePtId, GroupTypeID = GroupType1.ID, Culture = "pt", Name = "G 1 PT", ltl_GroupType = GroupType1, StatusBankID = StatusLive };
        protected static Group GroupWithTypeOneAndCultureEs = new Group { GroupID = GroupWithTypeOneAndCultureEsId, GroupTypeID = GroupType1.ID, Culture = "es", Name = "G 1 ES", ltl_GroupType = GroupType1, StatusBankID = StatusLive };


        protected List<Group> Groups = new List<Group>
        {
            GroupWithTypeOneAndCultureEn,
            GroupWithTypeOneAndCulturePt,
            GroupWithTypeOneAndCultureEs
        };
        protected List<GroupPermission> GroupPermissions = new List<GroupPermission>
        {
            new GroupPermission{ GroupID = GroupWithTypeOneAndCultureEn.GroupID, RoleID = CultureRoleEn.RoleID},
            new GroupPermission{ GroupID = GroupWithTypeOneAndCulturePt.GroupID, RoleID = CultureRolePt.RoleID},
            new GroupPermission{ GroupID = GroupWithTypeOneAndCultureEs.GroupID, RoleID = CultureRoleEs.RoleID},
            
        };

        //Country
        protected static int CountryEnId = 1;
        protected static int CountryPtId = 2;
        protected static int CountryEsId = 3;

        protected static int CountryEnRanking = 1;
        protected static int CountryPtRanking = 2;
        protected static int CountryEsRanking = 3;

        protected static Country CountryEn = new Country { CountryID = CountryEnId, CountryName = "Country EN", Ranking = CountryEnRanking };
        protected static Country CountryPt = new Country { CountryID = CountryPtId, CountryName = "Country PT", Ranking = CountryPtRanking };
        protected static Country CountryEs = new Country { CountryID = CountryEsId, CountryName = "Country ES", Ranking = CountryEsRanking };


        protected List<Country> Countries = new List<Country>
        {
            CountryEn,
            CountryPt,
            CountryEs
        }; 
 
        //User
        protected static int UserWithOneEnRoleId = 1;
        protected static int UserWithEnAndPtUserRolesId = 2;
        protected static int UserWithEnPtEsRolesId = 3;
        protected static int UserWithHiddenFromReportsRoleId = 4;
        protected static int UserWithMSLRoleId = 5;
        protected static int UserWithMedicalRoleId = 6;
        protected static int UserWithCommercialRoleId = 7;

        protected static User UserWithOneEnRole = new User { UserID = UserWithOneEnRoleId, CountryID = CountryEn.CountryID };
        protected static User UserWithEnAndPtUserRoles = new User { UserID = UserWithEnAndPtUserRolesId, CountryID = CountryEn.CountryID };
        protected static User UserWithEnPtEsRoles = new User { UserID = UserWithEnPtEsRolesId, CountryID = CountryEn.CountryID };
        protected static User UserWithEnAndHiddenFromReportsRole = new User { UserID = UserWithHiddenFromReportsRoleId, CountryID = CountryEn.CountryID };
        protected static User UserWithMSLRole = new User { UserID = UserWithMSLRoleId, CountryID = CountryEn.CountryID };
        protected static User UserWithMedicalRole = new User { UserID = UserWithMedicalRoleId, CountryID = CountryEn.CountryID };
        protected static User UserWithCommercialRole = new User { UserID = UserWithCommercialRoleId, CountryID = CountryEn.CountryID };

        protected List<User> Users = new List<User>
        {
           UserWithOneEnRole,
           UserWithEnAndPtUserRoles,
           UserWithEnPtEsRoles,
           UserWithEnAndHiddenFromReportsRole,
         
        };

        protected List<User> UsersWithJobRoles = new List<User>
        {
            UserWithMSLRole,
            UserWithCommercialRole,
            UserWithMedicalRole
        };
        protected static int JobRoleMslId = 83;
        protected static int JobRoleCommercialId = 84;
        protected static int JobRoleMedicalId = 7;
        protected static int JobRolesRoleGroupId = (int)RoleGroup.UserType;

        protected static Role JobRoleMSL = new Role
        {
            RoleID = JobRoleMslId,
            RoleGroupID = JobRolesRoleGroupId,
            Description = "",
            RoleName = "MSL"
        };
        protected static Role JobRoleCommercial = new Role
        {
            RoleID = JobRoleCommercialId,
            RoleGroupID = JobRolesRoleGroupId,
            Description = "",
            RoleName = "Commercial"
        };
        protected static Role JobRoleMedical = new Role
        {
            RoleID = JobRoleMedicalId,
            RoleGroupID = JobRolesRoleGroupId,
            Description = "",
            RoleName = "Medical"
        };
      



        protected List<UserRole> AllUserRoles = new List<UserRole>
        {
            new UserRole { UserID = UserWithOneEnRole.UserID, RoleID = CultureRoleEn.RoleID, askCore_Roles = CultureRoleEn, askCore_Users = UserWithOneEnRole },

            new UserRole {UserID = UserWithEnAndPtUserRoles.UserID, RoleID = CultureRoleEn.RoleID, askCore_Roles = CultureRoleEn, askCore_Users = UserWithEnAndPtUserRoles },
            new UserRole { UserID = UserWithEnAndPtUserRoles.UserID, RoleID = CultureRolePt.RoleID, askCore_Roles = CultureRolePt, askCore_Users = UserWithEnAndPtUserRoles },

            new UserRole { UserID = UserWithEnPtEsRoles.UserID, RoleID = CultureRoleEn.RoleID, askCore_Roles = CultureRoleEn, askCore_Users = UserWithEnPtEsRoles},
            new UserRole { UserID = UserWithEnPtEsRoles.UserID, RoleID = CultureRolePt.RoleID, askCore_Roles = CultureRolePt, askCore_Users = UserWithEnPtEsRoles},
            new UserRole { UserID = UserWithEnPtEsRoles.UserID, RoleID = CultureRoleEs.RoleID, askCore_Roles = CultureRoleEs, askCore_Users = UserWithEnPtEsRoles},

            new UserRole { UserID = UserWithEnAndHiddenFromReportsRole.UserID, RoleID = CultureRoleEn.RoleID, askCore_Roles = CultureRoleEn, askCore_Users = UserWithEnAndHiddenFromReportsRole},
            new UserRole { UserID = UserWithEnAndHiddenFromReportsRole.UserID, RoleID = RoleHiddenFromReports.RoleID, askCore_Roles = RoleHiddenFromReports, askCore_Users = UserWithEnAndHiddenFromReportsRole},

            new UserRole {UserID = UserWithMSLRole.UserID, RoleID = JobRoleMSL.RoleID, askCore_Roles =  JobRoleMSL, askCore_Users = UserWithMSLRole},
            new UserRole {UserID = UserWithMedicalRole.UserID, RoleID = JobRoleMedical.RoleID, askCore_Roles = JobRoleMedical, askCore_Users = UserWithMedicalRole },
            new UserRole {UserID = UserWithCommercialRole.UserID, RoleID = JobRoleCommercial.RoleID, askCore_Roles = JobRoleCommercial, askCore_Users =  UserWithCommercialRole}
        };

        

        //Role
        protected static int CultureRoleEnId = 1;
        protected static int CultureRolePtId = 2;
        protected static int CultureRoleEsId = 3;

      

        protected static int RoleHiddenFromReportsId = 4;

        protected static int CultureRolesRoleGroupId = (int)RoleGroup.CultureRoles;
       
        protected static int StandardRolesRoleGroupId = (int)RoleGroup.StandardRoles;

        protected static Role RoleHiddenFromReports = new Role { RoleID = RoleHiddenFromReportsId, RoleGroupID = StandardRolesRoleGroupId, Description = "VIP or Reader - training progress hidden from reports", RoleName = "HideUserFromReports" };
        protected static Role CultureRoleEn = new Role { RoleID = CultureRoleEnId, RoleGroupID = CultureRolesRoleGroupId, Description = "English - Global", RoleName = "en" };
        protected static Role CultureRolePt = new Role { RoleID = CultureRolePtId, RoleGroupID = CultureRolesRoleGroupId, Description = "Portuguese - Latin America", RoleName = "pt" };
        protected static Role CultureRoleEs = new Role { RoleID = CultureRoleEsId, RoleGroupID = CultureRolesRoleGroupId, Description = "Spanish - Latin America", RoleName = "es" };

      

        protected string JobRolesIds = "7,83,84";
        protected  List<int> JobFunctionIds = new List<int> {7,83,84};

    
        protected static List<Role> JobRoles = new List<Role> {JobRoleMSL, JobRoleCommercial};

        protected static List<Role> CultureRoles = new List<Role>
        {
            CultureRoleEn,
            CultureRolePt,
            CultureRoleEs

        };
        
        //Certificate
        protected int NumberOfUsersCertififedForGroupTypeByCountry = 11;
        protected List<CertificatesAchieved> CertificatesAchieved = new List<CertificatesAchieved>
        {
            new CertificatesAchieved{ Id = 1, GroupId = GroupTypeOneId, UserId = UserWithOneEnRoleId}
        };
      
        protected void PrepareSut()
        {
            var usersMoqDbSet = new MoqDbSetProvider<User>().DbSet(Users);
            var usersWithJobRolesMoqDbSet = new MoqDbSetProvider<User>().DbSet(UsersWithJobRoles);
            var userRolesMoqDbSet = new MoqDbSetProvider<UserRole>().DbSet(AllUserRoles);
            var groupsMoqDbSet = new MoqDbSetProvider<Group>().DbSet(Groups);
            var certificatesAchievedMoqDbSet = new MoqDbSetProvider<CertificatesAchieved>().DbSet(CertificatesAchieved);

            //BaseCommands
            BaseCommandsMock.Setup(m => m.GetByIdAsync<ltl_GroupType>(It.IsAny<int>())).ReturnsAsync(GroupType1);
            BaseCommandsMock.Setup(m => m.GetConditionalAsync(It.IsAny<Expression<Func<ltl_GroupType, bool>>>()))
                .ReturnsAsync(GroupTypes.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<Country>()).ReturnsAsync(Countries.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetConditionalAsync(It.IsAny<Expression<Func<UserRole, bool>>>())).ReturnsAsync(userRolesMoqDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetAllAsync<CertificatesAchieved>())
                .ReturnsAsync(certificatesAchievedMoqDbSet.Object);
            
            //GroupType
            GroupTypeCommandsMock.Setup(m => m.GetAllAvailableGroupTypes()).ReturnsAsync(GroupTypes);

            //Role
            RoleProviderMock.Setup(m => m.GetAllUserCultureRolesAsync()).ReturnsAsync(userRolesMoqDbSet.Object.Where(r => r.askCore_Roles.RoleGroupID == CultureRolesRoleGroupId));
            RoleProviderMock.Setup(m => m.GetAllCultureRoles()).ReturnsAsync(CultureRoles.AsQueryable());
          

            //UserRole

            //User
            AllowedUserFilterMock.Setup(m => m.GetUsersFilteredByCountry(It.Is<int>(x => x == CountryEn.CountryID), It.IsAny<IEnumerable<User>>()))
                .Returns(Users.Where(a => a.CountryID == CountryEn.CountryID).AsQueryable());

            AllowedUserFilterMock.Setup(m => m.GetUsersFilteredByCountry(It.Is<int>(x => x == CountryPt.CountryID), It.IsAny<IEnumerable<User>>()))
               .Returns(Users.Where(a => a.CountryID == CountryPt.CountryID).AsQueryable());

            AllowedUserFilterMock.Setup(m => m.GetUsersFilteredByCountry(It.Is<int>(x => x == CountryEs.CountryID), It.IsAny<IEnumerable<User>>()))
               .Returns(Users.Where(a => a.CountryID == CountryEs.CountryID).AsQueryable());


            AllowedUserFilterMock.Setup(m => m.GetAllLiveUsersNotHiddenFromReports())
                .ReturnsAsync(usersMoqDbSet.Object);

            AllowedUserFilterMock.Setup(
                m => m.GetUserIdsFilteredByJobFunctions(It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(UsersWithJobRoles);

            //Group
            FilterAllowedGroupsMock.Setup(m => m.GetAllLiveGroups()).ReturnsAsync(groupsMoqDbSet.Object);

            SUT = new OverviewCountryProgressCommands(BaseCommandsMock.Object,
                AllowedUserFilterMock.Object,
                CertificatesAchievedCommandsMock.Object,
                GroupTypeCommandsMock.Object,
                RoleProviderMock.Object, FilterAllowedGroupsMock.Object);
        }
    }
}
