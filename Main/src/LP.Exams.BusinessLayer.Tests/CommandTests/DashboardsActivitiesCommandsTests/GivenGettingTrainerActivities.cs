using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsActivitiesCommandsTests
{
    public class GivenGettingTrainerActivities : BaseGiven
    {
        private TrainerActivitiesContract _trainerActivitiesContract;

        private const int ExistingTrainerId = 1;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestAndThereAreTrainees : GivenGettingTrainerActivities
        {
            protected override async void When()
            {
                _trainerActivitiesContract = await SUT.GetTrainerActivities(ExistingTrainerId);
            }

            
            [Test]
            public void ThenGetAllFilteredUsersByTrainerIdIsCalledOnce()
            {
                FilterAllowedUserMock.Verify(m => m.GetUserIdsFilteredByTrainer(ExistingTrainerId), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUsersIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetCultureRolesForUsersIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.GetCultureRolesForUsers(It.IsAny<List<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetAllGroupsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Group>(), Times.Once());
            }

            [Test]
            public void ThenGetAllTrainingsExamAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync<TrainingsExam>(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetAttemptedExamIdsIsCalledOnce()
            {
                AttemptsCommnadsMock.Verify(m => m.GetAttempedExamIdsGroupedByUserIdList(It.IsAny<List<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetUsetPostsViewedIsCalledOnce()
            {
                UserPostViewedCommandsMock.Verify(m => m.GetUserPostsViewedGroupedList(It.IsAny<List<int>>()), Times.Once());
            }


            [Test]
            public void ThenGetCertificatesIsCalledOnce()
            {
                CertificatesAchievedCommands.Verify(m => m.GetCertificatesAchievedForUsersAndGroups(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenExpectedNumberOfRecordsIsReturned()
            {
                const int expected = 2;
                Assert.AreEqual(expected, _trainerActivitiesContract.TrainerActivityContract.Count);
            }
        }

        public class WhenMakingACorrectRequestAndThereIsNoTrainer : GivenGettingTrainerActivities
        {
            private const int NonExistingTrainerId = 100;
            protected override async void When()
            {
                FilteredUsers = new List<int>();
                PrepareSut();
                _trainerActivitiesContract = await SUT.GetTrainerActivities(NonExistingTrainerId);
            }

            [Test]
            public void ThenGetAllFilteredUsersByTrainerIdIsCalledOnce()
            {
                FilterAllowedUserMock.Verify(m => m.GetUserIdsFilteredByTrainer(NonExistingTrainerId), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUsersIsNeverCalled()
            {
                UserCommandsMock.Verify(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>()), Times.Never());
            }

            [Test]
            public void ThenGetCultureRolesForUsersIsNeverCalled()
            {
                UserRoleCommandsMock.Verify(m => m.GetCultureRolesForUsers(It.IsAny<List<int>>()), Times.Never());
            }

            [Test]
            public void ThenGetAllGroupsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Group>(), Times.Never());
            }

            [Test]
            public void ThenGetAllTrainingsExamAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainingsExam>(), Times.Never());
            }

            [Test]
            public void ThenGetAttemptedExamIdsIsNeverCalled()
            {
                AttemptsCommnadsMock.Verify(m => m.GetAttempedExamIdsGroupedByUserIdList(It.IsAny<List<int>>()), Times.Never());
            }

            [Test]
            public void ThenGetUsetPostsViewedIsNeverCalled()
            {
                UserPostViewedCommandsMock.Verify(m => m.GetUserPostsViewedGroupedList(It.IsAny<List<int>>()), Times.Never);
            }

            [Test]
            public void ThenGetCertificatesIsCalledOnce()
            {
                CertificatesAchievedCommands.Verify(m => m.GetCertificatesAchievedForUsersAndGroups(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<int>>()), Times.Never());
            }

            [Test]
            public void ThenOneRecordIsReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _trainerActivitiesContract.TrainerActivityContract.Count);
            }
        }

        public class WhenMakingACorrectRequestAndThereAreTrainersWithJobRole : GivenGettingTrainerActivities
        {
            protected int trainerId = 3;
            protected string jobRoleIds = "2,3";
            protected List<int> jobFunctionIds = new List<int> {2,3};
            protected override async void When()
            {
                    
                 PrepareSut();
                _trainerActivitiesContract = await SUT.GetTrainerActivities(trainerId,jobRoleIds);
            }

            [Test]
            public void ThenGetUsersFilteredByTrainerAndJobFunctionsIsCalledOnce()
            {
                FilterAllowedUserMock.Verify(m => m.GetUsersFilteredByTrainerAndJobFunctions(trainerId, jobFunctionIds),Times.Once);
            }
            [Test]
            public void ThenTheCorrectNumberOfTrainingActivitiesContractIsReturned()
            {
                Assert.AreEqual(_trainerActivitiesContract.TrainerActivityContract.Count,1);
            }

            [Test]
            public void ThenTheCorrectNumberOfUsersWithJobRolesIsReturned()
            {
                int expected = 2;
                Assert.AreEqual(expected,JobRoleUsers.Count);
            }

            [Test]
            public void ThenGetDecryptedUsersIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetCultureRolesForUsersIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.GetCultureRolesForUsers(It.IsAny<List<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetAllGroupsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Group>(), Times.Once());
            }

            [Test]
            public void ThenGetAllTrainingsExamAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync<TrainingsExam>(It.IsAny<Expression<Func<TrainingsExam, bool>>>(), It.IsAny<Expression<Func<TrainingsExam, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetAttemptedExamIdsIsCalledOnce()
            {
                AttemptsCommnadsMock.Verify(m => m.GetAttempedExamIdsGroupedByUserIdList(It.IsAny<List<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetUsetPostsViewedIsCalledOnce()
            {
                UserPostViewedCommandsMock.Verify(m => m.GetUserPostsViewedGroupedList(It.IsAny<List<int>>()), Times.Once());
            }


            [Test]
            public void ThenGetCertificatesIsCalledOnce()
            {
                CertificatesAchievedCommands.Verify(m => m.GetCertificatesAchievedForUsersAndGroups(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<int>>()), Times.Once());
            }
        }
    }
}
