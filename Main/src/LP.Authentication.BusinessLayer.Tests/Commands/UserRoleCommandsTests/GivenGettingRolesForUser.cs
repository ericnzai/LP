using System.Collections.Generic;
using LP.EntityModels;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class GivenGettingRolesForUser : BaseGiven
    {
        private List<UserRole> _userRoles;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserHasRoles : GivenGettingRolesForUser
        {
            protected override async void When()
            {
                _userRoles = await SUT.GetRolesForUserAsync(1);
            }

            [Test]
            public void ThenUserRolesIsNotNull()
            {
                Assert.IsNotNull(_userRoles);
            }
        }
    }
}
