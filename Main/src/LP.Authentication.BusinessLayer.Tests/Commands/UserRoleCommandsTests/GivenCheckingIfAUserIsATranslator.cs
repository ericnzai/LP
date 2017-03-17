using System.Collections.Generic;
using LP.EntityModels;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class GivenCheckingIfAUserIsATranslator : BaseGiven
    {
        private bool _isUserTranslator;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserShouldBeATranslator : GivenCheckingIfAUserIsATranslator
        {
            protected override void When()
            {
                _isUserTranslator = SUT.IsUserTranslator(new List<UserRole> { new UserRole { RoleID = 56, askCore_Roles = new Role { RoleName = "Admin_ContentTranslationManagement" } } });
            }

            [Test]
            public void TheIsUserTranslatorIsTrue()
            {
                Assert.True(_isUserTranslator);
            }
        }

        public class WhenTheUserShouldNotBeATranslator : GivenCheckingIfAUserIsATranslator
        {
            protected override void When()
            {
                _isUserTranslator = SUT.IsUserTranslator(new List<UserRole> { new UserRole { RoleID = 57, askCore_Roles = new Role { RoleName = "NotTranslator" } } });
            }

            [Test]
            public void TheIsUserTranslatorIsFalse()
            {
                Assert.False(_isUserTranslator);
            }
        }
    }
}
