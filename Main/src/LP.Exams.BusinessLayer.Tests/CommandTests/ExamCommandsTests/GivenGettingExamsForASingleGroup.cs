using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.EntityModels.Exam;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.ExamCommandsTests
{
    public class GivenGettingExamsForASingleGroup : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenARequestIsMadeForLiveExams : GivenGettingExamsForASingleGroup
        {
            private List<short> _examIds;

            private const int GroupId = 45;

            protected override async void When()
            {
                _examIds= await SUT.GetExamIdsForGroup(GroupId, true);
            }

            [Test]
            public void ThenGetTrainingsExamsConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>()), Times.Once());
            }

            [Test]
            public void TheExamIdsIsNotNull()
            {
                Assert.IsNotNull(_examIds);
            }

            [Test]
            public void ThenTheCorrectNumberOfExamIdsAreReturned()
            {
                const int expected = 22;

                Assert.AreEqual(expected, _examIds.Count);
            }

            [Test]
            public void ThenTheCorrectExamIdsAreReturned()
            {
                var expected = new List<short> {1, 3, 4, 6, 7, 9, 11, 12, 14, 15, 17, 19, 20, 22,24, 25, 27, 29, 30, 32, 34, 35};

                CollectionAssert.AreEquivalent(expected, _examIds);
            }
        }

        public class WhenARequestIsMadeForAllExams : GivenGettingExamsForASingleGroup
        {
            private List<short> _examIds;

            private const int GroupId = 734;

            protected override async void When()
            {
                _examIds = await SUT.GetExamIdsForGroup(GroupId, false);
            }

            [Test]
            public void ThenGetTrainingsExamsConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>()), Times.Once());
            }

            [Test]
            public void TheExamIdsIsNotNull()
            {
                Assert.IsNotNull(_examIds);
            }

            [Test]
            public void ThenTheCorrectNumberOfExamIdsAreReturned()
            {
                const int expected = 36;

                Assert.AreEqual(expected, _examIds.Count);
            }

            [Test]
            public void ThenTheCorrectExamIdsAreReturned()
            {
                var expected = new List<short> { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36 };

                CollectionAssert.AreEquivalent(expected, _examIds);
            }
        }
    }
}
