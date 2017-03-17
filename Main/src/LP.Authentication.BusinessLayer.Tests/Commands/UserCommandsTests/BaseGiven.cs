using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Authentication.BusinessLayer.Commands;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class BaseGiven : SpecsFor<UserCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IEncryptionHandler> EncryptionHandlerMock = new Mock<IEncryptionHandler>();
        protected readonly Mock<ICacheCommands> CacheCommandsMock = new Mock<ICacheCommands>();
        protected readonly Mock<IRoleCommands> RoleCommandsMock = new Mock<IRoleCommands>();
        protected readonly Mock<IUserRoleCommands> UserRoleCommandsMock = new Mock<IUserRoleCommands>();
        protected readonly Mock<IFilterAllowedUser> FilterAllowedUser = new Mock<IFilterAllowedUser>();
        protected const string DecryptedUserName = "decrypted@asandk.com";
        protected const string NotInDatabaseDecryptedUserName = "notindatabase@example.com";
        protected const string NotInDatabaseDecryptedDisplayUserName = "Not In Database";
        protected const string ExisingUserName = "existing@asandk.com";
        protected const string ExisingDisplayUserName = "Existing Name";
        protected const string AdminUserName = "theadminuser@asandk.com";
        protected const string TranslatorUserName = "theTranslatoruser@asandk.com";
        protected const string ExistingUserButNotDecryptedUserName = "notyetdecrypted@asandk.com";
        protected const string UserNotInDatabaseButStillInDecryptedUsersCacheUserName = "notindatabasebutstillcached@asandk.com";
        protected DecryptedUser DecryptedUser = new DecryptedUser
        {
            DecryptedUserName = DecryptedUserName,
            DecryptedDisplayName = GenericDecryptedUserDisplayName,
            UserId = 1
        };

        protected DecryptedUser NotInDatabaseDecryptedUser = new DecryptedUser
        {
            DecryptedUserName = NotInDatabaseDecryptedUserName,
            DecryptedDisplayName = NotInDatabaseDecryptedDisplayUserName,
            UserId = 2
        };

        protected DecryptedUser ExisingUser = new DecryptedUser
        {
            DecryptedUserName = ExisingUserName,
            DecryptedDisplayName = ExisingDisplayUserName,
            UserId = 3
        };

        protected DecryptedUser AdminUser = new DecryptedUser
        {
            DecryptedUserName = AdminUserName,
            UserId = 4
        };

        protected DecryptedUser TranslatorUser = new DecryptedUser
        {
            DecryptedUserName = TranslatorUserName,
            UserId = 5
        };

        protected DecryptedUser TestUser = new DecryptedUser
        {
            DecryptedUserName = "Test Test",
            UserId = 7
        };
        protected DecryptedUser UserNotInDatabaseButStillInDecryptedUsersCacheDecryptedUser = new DecryptedUser
        {
            DecryptedUserName = UserNotInDatabaseButStillInDecryptedUsersCacheUserName,
            UserId = 8
        };

        protected const string CorrectPassword = "correctpassword";
        protected const string IncorrectPassword = "incorrectpassword";

        protected const string GenericEncryptedUserDisplayName = "encrypted";
        protected const string GenericDecryptedUserDisplayName = "decrypted user name";

        protected const int ExistingTrainerId = 1;

        protected List<User> Users = new List<User>
        {
            new User { UserID = 3, UserName = ExisingUserName, Password = CorrectPassword, CountryID = 4, DisplayName = GenericEncryptedUserDisplayName, ParentID = ExistingTrainerId},
            new User { UserID = 4, UserName = AdminUserName, Password = CorrectPassword, CountryID = 4, DisplayName = GenericEncryptedUserDisplayName, ParentID = ExistingTrainerId},
            new User { UserID = 7, UserName = ExistingUserButNotDecryptedUserName, Password = CorrectPassword, CountryID = 4, DisplayName = GenericEncryptedUserDisplayName, ParentID = ExistingTrainerId},
            new User { UserID = 5, UserName = TranslatorUserName, Password = CorrectPassword, CountryID = 4, DisplayName = GenericEncryptedUserDisplayName, ParentID = 2}
        };

        protected List<UserRole> UserRoles = new List<UserRole>
        {
            new UserRole{UserID = 3, RoleID = 4, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 4}},
            new UserRole{UserID = 3, RoleID = 6, askCore_Roles = new Role(){RoleGroupID =  (int)RoleGroup.ExportRoles, RoleID = 6}},
            new UserRole{UserID = 3, RoleID = 8, askCore_Roles = new Role(){RoleGroupID =  (int)RoleGroup.UserType, RoleID = 8}},
            new UserRole{UserID = 3, RoleID = 9, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.CultureRoles, RoleID = 9}},
            new UserRole{UserID = 3, RoleID = 2, askCore_Roles = new Role(){RoleGroupID =  (int)RoleGroup.ReportRoles, RoleID = 2}},
            new UserRole{UserID = 3, RoleID = 1, askCore_Roles = new Role(){RoleGroupID =  (int)RoleGroup.AdminRoles, RoleID = 1}},
        };

        protected List<UserRole> AdminUserRoles = new List<UserRole>
        {
            new UserRole{UserID = 4, RoleID = 101, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 101}},
            new UserRole{UserID = 4, RoleID = 102, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 102}},
            new UserRole{UserID = 4, RoleID = 103, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 103}},
        };

        protected List<UserRole> TranslatorUserRoles = new List<UserRole>
        {
            new UserRole{UserID = 5, RoleID = 501, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 501}},
            new UserRole{UserID = 5, RoleID = 502, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 502}},
            new UserRole{UserID = 5, RoleID = 503, askCore_Roles = new Role(){RoleGroupID = (int)RoleGroup.StandardRoles, RoleID = 503}},
        };

        protected Country England = new Country
        {
            CountryID = 4,
            CountryName = "England"
        };

        protected List<User> UsersForCountTests = new List<User>
        {
            new User {UserID = 1, CountryID = 12, askCore_Countries = new Country {RegionId = 45}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 1}}},
            new User {UserID = 2, CountryID = 14, askCore_Countries = new Country {RegionId = 46}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 3}}},
            new User {UserID = 3, CountryID = 13, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 4}}},
            new User {UserID = 4, CountryID = 12, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 2}}},
            new User {UserID = 5, CountryID = 15, askCore_Countries = new Country {RegionId = 46}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 1}}},
            new User {UserID = 6, CountryID = 13, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 1}}},
            new User {UserID = 7, CountryID = 12, askCore_Countries = new Country {RegionId = 45}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 3}}},
            new User {UserID = 8, CountryID = 13, askCore_Countries = new Country {RegionId = 46}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 3}}},
            new User {UserID = 9, CountryID = 14, askCore_Countries = new Country {RegionId = 45}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 2}}},
            new User {UserID = 10, CountryID = 15, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 2}}},
            new User {UserID = 11, CountryID = 12, askCore_Countries = new Country {RegionId = 46}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 4}}},
            new User {UserID = 12, CountryID = 13, askCore_Countries = new Country {RegionId = 45}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 1}}},
            new User {UserID = 13, CountryID = 14, askCore_Countries = new Country {RegionId = 45}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 3}}},
            new User {UserID = 14, CountryID = 14, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 1}}},
            new User {UserID = 15, CountryID = 12, askCore_Countries = new Country {RegionId = 47}, askCore_UsersRoles = new List<UserRole>{new UserRole{RoleID = 2}}},
        }; 

        protected void PrepareSut()
        {
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(It.IsAny<int>())).Returns(DecryptedUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(DecryptedUserName)).Returns(DecryptedUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(NotInDatabaseDecryptedUserName)).Returns(NotInDatabaseDecryptedUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(ExisingUserName)).Returns(ExisingUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(AdminUserName)).Returns(AdminUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(TranslatorUserName)).Returns(TranslatorUser);
            CacheCommandsMock.Setup(m => m.GetDecryptedUser(UserNotInDatabaseButStillInDecryptedUsersCacheUserName)).Returns(UserNotInDatabaseButStillInDecryptedUsersCacheDecryptedUser);

            var userMoqDbSetProvider = new MoqDbSetProvider<User>();
            var userMoqDbSet = userMoqDbSetProvider.DbSet(Users);

            BaseCommandsMock.Setup(m => m.GetAllAsync<User>()).ReturnsAsync(userMoqDbSet.Object);

            BaseCommandsMock.Setup(m => m.GetAllAsync<UserRole>()).ReturnsAsync(UserRoles.AsQueryable());

            BaseCommandsMock.Setup(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 3))).ReturnsAsync(Users.FirstOrDefault(x => x.UserID == 3));
            BaseCommandsMock.Setup(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 7))).ReturnsAsync(Users.FirstOrDefault(x => x.UserID == 7));

            BaseCommandsMock.Setup(m => m.GetByIdAsync<Country>(It.Is<int>(x => x == 4))).ReturnsAsync(England);

            EncryptionHandlerMock.Setup(m => m.DecryptString(It.Is<string>(x => x == CorrectPassword)))
                .Returns(CorrectPassword);

            EncryptionHandlerMock.Setup(m => m.DecryptString(It.Is<string>(x => x == GenericEncryptedUserDisplayName)))
               .Returns(GenericDecryptedUserDisplayName);

            RoleCommandsMock.Setup(m => m.GetFieldOfEmployment(It.IsAny<ICollection<int>>())).ReturnsAsync("MSL");

            UserRoleCommandsMock.Setup(m => m.GetRolesForUserAsync(It.IsAny<int>())).ReturnsAsync(UserRoles);

            UserRoleCommandsMock.Setup(m => m.GetRolesForUserAsync(It.Is<int>(x => x == 4))).ReturnsAsync(AdminUserRoles);

            UserRoleCommandsMock.Setup(m => m.GetRolesForUserAsync(It.Is<int>(x => x == 5))).ReturnsAsync(TranslatorUserRoles);

            UserRoleCommandsMock.Setup(m => m.IsUserAdmin(It.IsAny<List<UserRole>>())).Returns(false);

            UserRoleCommandsMock.Setup(m => m.IsUserTranslator(It.IsAny<List<UserRole>>())).Returns(false);

            UserRoleCommandsMock.Setup(m => m.IsUserAdmin(It.Is<List<UserRole>>(x => x == AdminUserRoles))).Returns(true);

            UserRoleCommandsMock.Setup(m => m.IsUserTranslator(It.Is<List<UserRole>>(x => x == TranslatorUserRoles))).Returns(true);

            var moqUserDbSet = new MoqDbSetProvider<User>().DbSet(UsersForCountTests);

            FilterAllowedUser.Setup(m => m.GetAllLiveUsersNotHiddenFromReports())
                .ReturnsAsync(moqUserDbSet.Object);

            SUT = new UserCommands(BaseCommandsMock.Object, EncryptionHandlerMock.Object, CacheCommandsMock.Object, RoleCommandsMock.Object, UserRoleCommandsMock.Object, FilterAllowedUser.Object);
        }
    }
}
