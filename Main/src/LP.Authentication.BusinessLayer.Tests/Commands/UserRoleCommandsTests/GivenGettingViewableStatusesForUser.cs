using System.Collections.Generic;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserRoleCommandsTests
{
    public class GivenGettingViewableStatusesForUser : BaseGiven
    {
        private List<int> _roleIds;

        private readonly List<int> _allRoleIds = new List<int> { (int)Status.Live, (int)Status.ComingSoon, (int)Status.TranslationInProgress };
        private readonly List<int> _normalRoleIds = new List<int> { (int)Status.Live, (int)Status.ComingSoon };
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserIsAnAdminAndATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(true, true);
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenTheUserIsAnAdmin : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(true, false);
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenTheUserIsATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(false, true);
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenTheUserIsNotAnAdminOrATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(false, false);
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenTwoRoleIdsShouldBeReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_normalRoleIds, _roleIds);
            }
        }

        public class WhenUserDetailsArePassedAndTheUserIsAnAdminAndATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(new UserDetails{IsAdmin = true, IsTranslator = true});
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenUserDetailsArePassedAndTheUserIsAnAdmin : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser( new UserDetails {IsAdmin = true, IsTranslator = false });
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenUserDetailsArePassedAndTheUserIsATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(new UserDetails {IsAdmin = false, IsTranslator = true} );
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenThreeRoleIdsShouldBeReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_allRoleIds, _roleIds);
            }
        }

        public class WhenUserDetailsArePassedAndTheUserIsNotAnAdminOrATranslator : GivenGettingViewableStatusesForUser
        {
            protected override void When()
            {
                _roleIds = SUT.GetViewableStatusesForUser(new UserDetails { IsAdmin = false, IsTranslator = false });
            }

            [Test]
            public void ThenRoleIdsIsNotNull()
            {
                Assert.IsNotNull(_roleIds);
            }

            [Test]
            public void ThenTwoRoleIdsShouldBeReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _roleIds.Count);
            }

            [Test]
            public void ThenRoleIdsShouldBeCorrect()
            {
                CollectionAssert.AreEquivalent(_normalRoleIds, _roleIds);
            }
        }
    }
}
