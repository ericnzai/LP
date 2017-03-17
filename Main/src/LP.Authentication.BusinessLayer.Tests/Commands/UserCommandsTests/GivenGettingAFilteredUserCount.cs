using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenGettingAFilteredUserCount : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenNoValuesArePassed : GivenGettingAFilteredUserCount
        {
            private int _userCount;

            protected override async void When()
            {
                _userCount = await SUT.GetUserCountFiltered();
            }

            [Test]
            public void ThenFilterAllowedUserGetAllLiveUsersNotHiddenFromReportsIsCalledOnce()
            {
                FilterAllowedUser.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Once());
            }

            [Test]
            public void ThenUserCountIsCorrect()
            {
                const int expected = 15;

                Assert.AreEqual(expected, _userCount);
            }
        }

        public class WhenOnlyARegionIdValueIsPassed : GivenGettingAFilteredUserCount
        {
            private int _userCount;

            protected override async void When()
            {
                _userCount = await SUT.GetUserCountFiltered(regionId:45);
            }

            [Test]
            public void ThenFilterAllowedUserGetAllLiveUsersNotHiddenFromReportsIsCalledOnce()
            {
                FilterAllowedUser.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Once());
            }

            [Test]
            public void ThenUserCountIsCorrect()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _userCount);
            }
        }

        public class WhenOnlyARegionIdOf46ValueIsPassed : GivenGettingAFilteredUserCount
        {
            private int _userCount;

            protected override async void When()
            {
                _userCount = await SUT.GetUserCountFiltered(regionId: 46);
            }

            [Test]
            public void ThenFilterAllowedUserGetAllLiveUsersNotHiddenFromReportsIsCalledOnce()
            {
                FilterAllowedUser.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Once());
            }

            [Test]
            public void ThenUserCountIsCorrect()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _userCount);
            }
        }

        public class WhenOnlyARegionIdOf47ValueIsPassed : GivenGettingAFilteredUserCount
        {
            private int _userCount;

            protected override async void When()
            {
                _userCount = await SUT.GetUserCountFiltered(regionId: 47);
            }

            [Test]
            public void ThenFilterAllowedUserGetAllLiveUsersNotHiddenFromReportsIsCalledOnce()
            {
                FilterAllowedUser.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Once());
            }

            [Test]
            public void ThenUserCountIsCorrect()
            {
                const int expected = 6;

                Assert.AreEqual(expected, _userCount);
            }
        }
    }
}
