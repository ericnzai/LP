using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class GivenGettingCultureRolesForUser : BaseGiven
    {
        private IEnumerable<Role> _roleIds;
        private const int ExistingUserId = 1;
        private const int UserWithNoCultureRoleId = 3;
        private const int CultureRoleId = 1;
        private const int AnotherCultureRoleId = 2;
        protected override void Given()
        {
            UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    UserID = ExistingUserId,
                    RoleID = CultureRoleId,
                },
                new UserRole
                {
                    UserID = ExistingUserId,
                    RoleID = AnotherCultureRoleId,
                },
                new UserRole
                {
                    UserID = ExistingUserId,
                    RoleID = 3,
                },
                new UserRole
                {
                    UserID = 2,
                    RoleID = CultureRoleId,
                },
                new UserRole
                {
                    UserID = 2,
                    RoleID = 3,
                },
                 new UserRole
                {
                    UserID = UserWithNoCultureRoleId,
                    RoleID = 3,
                },
            };

            Roles = new List<Role>
            {
                new Role{RoleID = CultureRoleId, RoleName = "Culture Role 1", RoleGroupID = 1},
                new Role{RoleID = AnotherCultureRoleId, RoleName = "Culture Role 2", RoleGroupID = 1},
                new Role{RoleID = 3, RoleName = "Role", RoleGroupID = 2},
            };
            RoleGroups = new List<askCore_RoleGroup>
            {
                new askCore_RoleGroup{RoleGroupID = 1, RoleGroupName = RoleGroup.CultureRoles.ToString()},
                new askCore_RoleGroup{RoleGroupID = 2, RoleGroupName = "OtherRoles"},
            };
            PrepareSut();
        }

        public class WhenCorrectReqestIsMade : GivenGettingCultureRolesForUser
        {
            protected override async void When()
            {
                _roleIds = await SUT.GetCultureRolesForUserAsync(It.IsAny<int>());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<UserRole>(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Once);
            }
        }

        public class WhenUserHasAssignedCultureRoles : GivenGettingCultureRolesForUser
        {
            protected override async void When()
            {
                UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    UserID = ExistingUserId,
                    RoleID = CultureRoleId,
                },
                new UserRole
                {
                    UserID = ExistingUserId,
                    RoleID = AnotherCultureRoleId,
                },

            };

                PrepareSut();

                _roleIds = await SUT.GetCultureRolesForUserAsync(ExistingUserId);
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<UserRole>(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenExpectedNumberOfRolesShouldBeReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _roleIds.Count());
            }
        }

        public class WhenTheUserHasNoCultureRole : GivenGettingCultureRolesForUser
        {
            protected override async void When()
            {
                UserRoles = new List<UserRole>
            {   
                new UserRole
                {
                    UserID = UserWithNoCultureRoleId,
                },
            };

                PrepareSut();

                _roleIds = await SUT.GetCultureRolesForUserAsync(UserWithNoCultureRoleId);
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<UserRole>(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Once);
            }


            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenNoRoleShouldBeReturned()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _roleIds.Count());
            }
        }

        public class WhenTheRequestIsMadeWithIncorrectParameter : GivenGettingCultureRolesForUser
        {
            protected override async void When()
            {
                UserRoles = new List<UserRole>();

                PrepareSut();

                _roleIds = await SUT.GetCultureRolesForUserAsync(500);
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<UserRole>(It.IsAny<Expression<Func<UserRole, bool>>>()), Times.Once);
            }

            [Test]
            public void ThenNoRoleShouldBeReturned()
            {
                Assert.IsNull(_roleIds);
            }
        }
    }
}
