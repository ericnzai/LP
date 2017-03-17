using System.Collections.Generic;
using LP.EntityModels;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class GivenCheckingIfAUserIsAdmin : BaseGiven
    {
        private bool _isUserAdmin;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserShouldBeAnAdmin : GivenCheckingIfAUserIsAdmin
        {
            protected override void When()
            {
                _isUserAdmin = SUT.IsUserAdmin(new List<UserRole> { new UserRole { RoleID = 56, askCore_Roles = new Role { RoleName = "Administrator" } } });
            }

            [Test]
            public void TheIsUserAdminIsTrue()
            {
                Assert.True(_isUserAdmin);
            }
        }

        public class WhenTheUserShouldNotBeAnAdmin : GivenCheckingIfAUserIsAdmin
        {
            protected override void When()
            {
                _isUserAdmin = SUT.IsUserAdmin(new List<UserRole> { new UserRole { RoleID = 57, askCore_Roles = new Role { RoleName = "NotAdmin" } } });
            }

            [Test]
            public void TheIsUserAdminIsFalse()
            {
                Assert.False(_isUserAdmin);
            }
        }
    }
}
