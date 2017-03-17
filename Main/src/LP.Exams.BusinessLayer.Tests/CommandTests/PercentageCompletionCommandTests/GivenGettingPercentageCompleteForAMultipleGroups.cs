using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.PercentageCompletionCommandTests
{
    public class GivenGettingPercentageCompleteForAMultipleGroups : BaseGiven
    {
        private List<GroupPercentageComplete> _groupsPercentageComplete;
        private GroupPercentageComplete _firstGroupPercentageComplete;
        private GroupPercentageComplete _secondGroupPercentageComplete;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenThereAreMultipleGroupsTheRepositoriesAreCalledCorrectly : GivenGettingPercentageCompleteForAMultipleGroups
        {
            protected override async void When()
            {
                _groupsPercentageComplete = await SUT.PercentageAchievedForGroups(UserId, new List<int> { 56, 58 });
            }

            [Test]
            public void ThenGetExamIdsForGroupIsCalledTheCorrectAmountOfTimes()
            {
                ExamCommandsMock.Verify(m => m.GetExamIdsForGroup(It.IsAny<int>(), It.IsAny<bool>()), Times.Exactly(2));
            }

            [Test]
            public void ThenGetExamIdsForGroupIsCalledOnceForTheFirstGroup()
            {
                ExamCommandsMock.Verify(m => m.GetExamIdsForGroup(It.Is<int>(x => x == 56), It.IsAny<bool>()), Times.Once());
            }

            [Test]
            public void ThenGetExamIdsForGroupIsCalledOnceForTheSecondGroup()
            {
                ExamCommandsMock.Verify(m => m.GetExamIdsForGroup(It.Is<int>(x => x == 58), It.IsAny<bool>()), Times.Once());
            }

            [Test]
            public void ThenGetGroupByIdAsyncIsCalledTheCorrectAmountOfTimes()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.IsAny<int>()), Times.Exactly(2));
            }

            [Test]
            public void ThenGetGroupByIdAsyncIsCalledOnceForTheFirstGroup()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.Is<int>(x=> x == 56)), Times.Once());
            }

            [Test]
            public void ThenGetGroupByIdAsyncIsCalledOnceForTheSecondGroup()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.Is<int>(x => x == 58)), Times.Once());
            }

            [Test]
            public void ThenCalculatePercentagesIsCalledTheCorrectAmountOfTimes()
            {
                CommonCalculatorCommandsMock.Verify(m => m.CalculatePercentages(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(2));
            }
        }

        public class WhenThereAreMultipleGroups : GivenGettingPercentageCompleteForAMultipleGroups
        {
            protected override async void When()
            {
                _groupsPercentageComplete = await SUT.PercentageAchievedForGroups(UserId, new List<int>{56, 58});

                _firstGroupPercentageComplete = _groupsPercentageComplete.First(a => a.GroupId == 56);

                _secondGroupPercentageComplete = _groupsPercentageComplete.First(a => a.GroupId == 58);
            }

            [Test]
            public void ThenGroupsPercentageCompleteIsNotNull()
            {
                Assert.IsNotNull(_groupsPercentageComplete);
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupsAreReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _groupsPercentageComplete.Count);
            }

            [Test]
            public void ThenTheFirstGroupPercentageCompleteGroupNameIsCorrect()
            {
                const string expected = "Test Group 1";

                Assert.AreEqual(expected, _firstGroupPercentageComplete.GroupName);
            }

            [Test]
            public void ThenTheSecondGroupPercentageCompleteGroupNameIsCorrect()
            {
                const string expected = "Test Group 2";

                Assert.AreEqual(expected, _secondGroupPercentageComplete.GroupName);
            }

            [Test]
            public void ThenTheFirstGroupPercentageCompletePercentageCompleteIsCorrect()
            {
                const int expected = 60;

                Assert.AreEqual(expected, _firstGroupPercentageComplete.PercentageComplete);
            }

            [Test]
            public void ThenTheSecondGroupPercentageCompletePercentageCompleteIsCorrect()
            {
                const int expected = 70;

                Assert.AreEqual(expected, _secondGroupPercentageComplete.PercentageComplete);
            }
        }
    }
}
