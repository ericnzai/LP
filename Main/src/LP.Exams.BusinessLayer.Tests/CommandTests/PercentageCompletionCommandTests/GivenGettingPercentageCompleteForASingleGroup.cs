using System.Collections.Generic;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.PercentageCompletionCommandTests
{
    public class GivenGettingPercentageCompleteForASingleGroup : BaseGiven
    {
        private GroupPercentageComplete _groupPercentageComplete;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheGroupHasSomeProgress : GivenGettingPercentageCompleteForASingleGroup
        {
            protected override async void When()
            {
                _groupPercentageComplete = await SUT.PercentageAchievedForGroup(UserId, GroupId1);
            }

            [Test]
            public void ThenExamCommandsGetExamIdsForGroupIsCalledOnce()
            {
                ExamCommandsMock.Verify(m => m.GetExamIdsForGroup(It.IsAny<int>(), It.IsAny<bool>()), Times.Once());
            }

            [Test]
            public void ThenExamCommandsGetExamIdsForGroupIsCalledOnceWithTheCorrectParameters()
            {
                ExamCommandsMock.Verify(m => m.GetExamIdsForGroup(It.Is<int>(x => x == GroupId1), It.IsAny<bool>()), Times.Once());
            }

            [Test]
            public void ThenAttemptsCommandsGetPassedAttemptsForUserIsCalledOnce()
            {
                AttemptsCommandsMock.Verify(m => m.GetPassedAttemptsForUser(It.IsAny<int>(), It.IsAny<IEnumerable<short>>()), Times.Once());
            }

            [Test]
            public void ThenAttemptsCommandsGetPassedAttemptsForUserIsCalledOnceWithTheCorrectParameters()
            {
                AttemptsCommandsMock.Verify(m => m.GetPassedAttemptsForUser(It.Is<int>(x => x == UserId), It.IsAny<IEnumerable<short>>()), Times.Once());
            }

            [Test]
            public void ThenGroupCommandsGetByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGroupCommandsGetByIdAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.Is<int>(x => x == GroupId1)), Times.Once());
            }

            [Test]
            public void ThenCommonCalculatorCommandsCalculatePercentagesIsCalledOnce()
            {
                CommonCalculatorCommandsMock.Verify(m => m.CalculatePercentages(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGroupPercentageCompleteIsNotNull()
            {
                Assert.IsNotNull(_groupPercentageComplete);
            }

            [Test]
            public void ThenGroupIdIsReturnedCorrectly()
            {
                Assert.AreEqual(GroupId1, _groupPercentageComplete.GroupId);
            }

            [Test]
            public void ThenGroupNameIsReturnedCorrectly()
            {
                Assert.AreEqual(GroupName1, _groupPercentageComplete.GroupName);
            }
        }
    }
}
