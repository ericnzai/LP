using System.Collections.Generic;
using LP.EntityModels;
using LP.EntityModels.Exam;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.AttemptsCommandsTests
{
    public class GivenGettingAttemptsPassed : BaseGiven
    {
        private List<short> _examIds;
        private int _calculation;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserHasAttemptsButNoneArePassed : GivenGettingAttemptsPassed
        {
            protected override async void When()
            {
                _examIds = await SUT.GetPassedAttemptsForUser(1);
            }

            [Test]
            public void ThenGetAllAttemptsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
            }

            [Test]
            public void ThenExamIdsIsNotNull()
            {
                Assert.IsNotNull(_examIds);
            }

            [Test]
            public void ThenNoExamIdsAreReturned()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _examIds.Count);
            }


            public class WhenTheUserHasAttemptsButNoneArePassedAndTheExamIdsOverloadIsUsed : GivenGettingAttemptsPassed
            {
                protected override async void When()
                {
                    _examIds = await SUT.GetPassedAttemptsForUser(1, new List<short> {1, 2, 3, 4, 5});
                }

                [Test]
                public void ThenGetAllAttemptsAsyncIsCalledOnce()
                {
                    BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
                }

                [Test]
                public void ThenExamIdsIsNotNull()
                {
                    Assert.IsNotNull(_examIds);
                }

                [Test]
                public void ThenNoExamIdsAreReturned()
                {
                    const int expected = 0;

                    Assert.AreEqual(expected, _examIds.Count);
                }
            }

            public class WhenTheUserHasAttemptsAndSomeArePassed : GivenGettingAttemptsPassed
            {
                protected override async void When()
                {
                    _examIds = await SUT.GetPassedAttemptsForUser(2);
                }

                [Test]
                public void ThenGetAllAttemptsAsyncIsCalledOnce()
                {
                    BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
                }

                [Test]
                public void ThenExamIdsIsNotNull()
                {
                    Assert.IsNotNull(_examIds);
                }

                [Test]
                public void ThenTheCorrectAmountOfExamIdsAreReturned()
                {
                    const int expected = 3;

                    Assert.AreEqual(expected, _examIds.Count);
                }

                [Test]
                public void ThenTheCorrectExamIdsAreReturned()
                {
                    var expected = new List<short> {1, 3, 5};

                    CollectionAssert.AreEquivalent(expected, _examIds);
                }
            }

            public class WhenTheUserHasAttemptsAndSomeArePassedAndTheExamIdsOverloadIsUsed : GivenGettingAttemptsPassed
            {
                protected override async void When()
                {
                    _examIds = await SUT.GetPassedAttemptsForUser(2, new List<short>{1,2,3,4,5});
                }

                [Test]
                public void ThenGetAllAttemptsAsyncIsCalledOnce()
                {
                    BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
                }

                [Test]
                public void ThenExamIdsIsNotNull()
                {
                    Assert.IsNotNull(_examIds);
                }

                [Test]
                public void ThenTheCorrectAmountOfExamIdsAreReturned()
                {
                    const int expected = 3;

                    Assert.AreEqual(expected, _examIds.Count);
                }

                [Test]
                public void ThenTheCorrectExamIdsAreReturned()
                {
                    var expected = new List<short> { 1, 3, 5 };

                    CollectionAssert.AreEquivalent(expected, _examIds);
                }
            }

            public class WhenTheUserHasAttemptsAndSomeArePassedButNotAllTheExamsArePassedAsAnOverload : GivenGettingAttemptsPassed
            {
                protected override async void When()
                {
                    _examIds = await SUT.GetPassedAttemptsForUser(2, new List<short> { 1, 2 });
                }

                [Test]
                public void ThenGetAllAttemptsAsyncIsCalledOnce()
                {
                    BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
                }

                [Test]
                public void ThenExamIdsIsNotNull()
                {
                    Assert.IsNotNull(_examIds);
                }

                [Test]
                public void ThenTheCorrectAmountOfExamIdsAreReturned()
                {
                    const int expected = 1;

                    Assert.AreEqual(expected, _examIds.Count);
                }

                [Test]
                public void ThenTheCorrectExamIdsAreReturned()
                {
                    var expected = new List<short> { 1 };

                    CollectionAssert.AreEquivalent(expected, _examIds);
                }
            }

            public class WhenTheGroupTypeHasNoUsers : GivenGettingAttemptsPassed
            {
                protected override async void When()
                {
                    _calculation = await SUT.GetNumberOfUsersWithSelfAssessmentInProgressForGroupType(1);
                }

                [Test]
                public void ThenGetNumberOfUsersWithSelfAssessmentInProgressForGroupTypeIsCalledOnce()
                {
                    BaseCommandsMock.Verify(m => m.GetAllAsync<Attempt>(), Times.Once());
                }
            }
        }
    }
}
