using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupPermissionCommandsTests
{
    public class GivenGettingGroupsWithPermissionsForRoles : BaseGiven
    {
        private IEnumerable<Group> _groups;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserHasMultipleAvailableStatuses : GivenGettingGroupsWithPermissionsForRoles
        {
            protected override async void When()
            {
                _groups = await SUT.GroupsWithPermissionsForRoles(RoleIds, AvailableStatuses);
            }

            [Test]
            public void ThenGetGroupPermissionsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<GroupPermission, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGroupsIsNotNull()
            {
                Assert.IsNotNull(_groups);
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupsAreReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _groups.Count());
            }

            [Test]
            public void ThenTheCorrectGroupsAreReturned()
            {
                var expected = new List<int> {7, 8};

                var actual = _groups.Select(g => g.GroupID);

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }
    }
}
