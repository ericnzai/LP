using System.Collections.Generic;
using System.Linq;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.NumberAchievedCommandsTests
{
    public class GivenGettingTheNumberOfModulesCompletedForAllTrainingAreas : BaseGiven
    {
        private List<TrainingAreaCompletion> _trainingAreaCompletions;
        private UserDetails _userDetails = new UserDetails {UserId = 1};

        private TrainingAreaCompletion _firstTrainingAreaCompletion;
        private TrainingAreaCompletion _secondTrainingAreaCompletion;
        private TrainingAreaCompletion _thirdTrainingAreaCompletion;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCheckingTheDependenciesAreCalledCorrectly : GivenGettingTheNumberOfModulesCompletedForAllTrainingAreas
        {
            protected override async void When()
            {
                _trainingAreaCompletions = await SUT.NumberOfModulesCompletedForAllTrainingAreasAsync(_userDetails);

                _firstTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 1);
                _secondTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 2);
                _thirdTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 3);
            }

            [Test]
            public void ThenNumberOfModulesCompletedForTrainingAreaIsCalledTheCorrectAmountOfTimes()
            {
                GroupPermissionCommandsMock.Verify(m =>
                    m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.IsAny<int>(),
                        It.IsAny<IEnumerable<int>>()), Times.Exactly(3));
            }

            [Test]
            public void ThenNumberOfModulesCompletedForTrainingAreaIsCalledOnceForTheFirstTrainingArea()
            {
                GroupPermissionCommandsMock.Verify(m =>
                    m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 1),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenNumberOfModulesCompletedForTrainingAreaIsCalledOnceForTheSecondTrainingArea()
            {
                GroupPermissionCommandsMock.Verify(m =>
                    m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 2),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenNumberOfModulesCompletedForTrainingAreaIsCalledOnceForTheThirdTrainingArea()
            {
                GroupPermissionCommandsMock.Verify(m =>
                    m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 3),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetCertificatesAchievedByUserForGroupsIsCalledTheCorrectAmountOfTimes()
            {
                PercentageCompletionCommandsMock.Verify(m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Exactly(3));
            }
        }

        public class WhenCalledWithAUserThatHasAccessToANumberOfGroups : GivenGettingTheNumberOfModulesCompletedForAllTrainingAreas
        {
            protected override async void When()
            {
                _trainingAreaCompletions = await SUT.NumberOfModulesCompletedForAllTrainingAreasAsync(_userDetails);

                _firstTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 1);
                _secondTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 2);
                _thirdTrainingAreaCompletion = _trainingAreaCompletions.First(a => a.TrainingAreaId == 3);
            }

            [Test]
            public void ThenTrainingAreaCompletionsIsNotNull()
            {
                Assert.IsNotNull(_trainingAreaCompletions);
            }

            [Test]
            public void ThenTheCorrectAmountOfTrainingAreasAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _trainingAreaCompletions.Count);
            }

            [Test]
            public void ThenTheCorrectTrainingAreasAreReturned()
            {
                var expected = new List<int> {1, 2, 3};

                CollectionAssert.AreEquivalent(expected, _trainingAreaCompletions.Select(t => t.TrainingAreaId));
            }

            [Test]
            public void ThenFirstTrainingAreaHasTheCorrectTotalNumberOfModules()
            {
                const int expected = 15;

                Assert.AreEqual(expected, _firstTrainingAreaCompletion.TotalNumberOfModules);
            }

            [Test]
            public void ThenFirstTrainingAreaHasTheCorrectNumberOfModulesComplete()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _firstTrainingAreaCompletion.NumberOfModulesComplete);
            }

            [Test]
            public void ThenSecondTrainingAreaHasTheCorrectTotalNumberOfModules()
            {
                const int expected = 9;

                Assert.AreEqual(expected, _secondTrainingAreaCompletion.TotalNumberOfModules);
            }

            [Test]
            public void ThenSecondTrainingAreaHasTheCorrectNumberOfModulesComplete()
            {
                const int expected = 7;

                Assert.AreEqual(expected, _secondTrainingAreaCompletion.NumberOfModulesComplete);
            }

            [Test]
            public void ThenThirdTrainingAreaHasTheCorrectTotalNumberOfModules()
            {
                const int expected = 6;

                Assert.AreEqual(expected, _thirdTrainingAreaCompletion.TotalNumberOfModules);
            }

            [Test]
            public void ThenThirdTrainingAreaHasTheCorrectNumberOfModulesComplete()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _thirdTrainingAreaCompletion.NumberOfModulesComplete);
            }
        }
    }
}
