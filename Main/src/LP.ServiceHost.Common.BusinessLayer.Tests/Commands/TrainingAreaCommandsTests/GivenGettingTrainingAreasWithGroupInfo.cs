using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainingAreaCommandsTests
{
    public class GivenGettingTrainingAreasWithGroupInfo : BaseGiven
    {
        private IQueryable<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>>
            _trainingAreasGroupedByGroupTypeAndGroup;

        private IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>
            _trainingAreaGroupedByGroupTypeAndGroup;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingMultipleTrainingAreas : GivenGettingTrainingAreasWithGroupInfo
        {
            protected override async void When()
            {
                _trainingAreasGroupedByGroupTypeAndGroup = await SUT.GetTrainingAreasWithAllGroupInfo(new List<int>(), AccessType.DisplayOnDashboards);
            }

            [Test]
            public void ThenTrainingAreasGroupedByGroupTypeAndGroupIsNotNull()
            {
                Assert.IsNotNull(_trainingAreasGroupedByGroupTypeAndGroup);
            }

            [Test]
            public void ThenGetGroupsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<Group>(It.IsAny<Expression<Func<Group, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByStatusOfAsyncTrainingAreaIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByStatusOfAsync<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByPermissionsOfTrainingAreaIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByPermissionsOf<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByStatusOfAsyncGroupIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByStatusOfAsync<Group>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByPermissionsOfGroupIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByPermissionsOf<Group>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupsAreReturned()
            {
                const int expected = 6;

                Assert.AreEqual(expected, _trainingAreasGroupedByGroupTypeAndGroup.Select(k => k.Key).Count());
            }
        }

        public class WhenGettingASingleTrainingArea : GivenGettingTrainingAreasWithGroupInfo
        {
            protected override async void When()
            {
                _trainingAreaGroupedByGroupTypeAndGroup = await SUT.GetTrainingAreaWithAllGroupInfo(1, new List<int>(), AccessType.DisplayOnDashboards);
            }

            [Test]
            public void ThenTrainingAreaGroupedByGroupTypeAndGroupIsNotNull()
            {
                Assert.IsNotNull(_trainingAreaGroupedByGroupTypeAndGroup);
            }

            [Test]
            public void ThenGetGroupsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<Group>(It.IsAny<Expression<Func<Group, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByStatusOfAsyncTrainingAreaIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByStatusOfAsync<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByPermissionsOfTrainingAreaIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByPermissionsOf<TrainingArea>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByStatusOfAsyncGroupIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByStatusOfAsync<Group>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>(), It.IsAny<AccessType>()), Times.Once());
            }

            [Test]
            public void ThenTrainingGroupPermissionFilterGetAvailableByPermissionsOfGroupIsCalledOnce()
            {
                TrainingGroupPermissionFilterMock.Verify(m => m.GetAvailableByPermissionsOf<Group>(It.IsAny<IQueryable<Group>>(),
                        It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenTheCorrectAmountOfTrainingAreasAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _trainingAreaGroupedByGroupTypeAndGroup.Select(x => x).Count());
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupTypesAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _trainingAreaGroupedByGroupTypeAndGroup.Select(k => k.Key).Count());
            }

            //[Test]
            //public void ThenTheCorrectAmountOfGroupsAreReturned()
            //{
            //    const int expected = 1;

            //    Assert.AreEqual(expected, _trainingAreaGroupedByGroupTypeAndGroup.Select(k => k.Key).SelectMany(x => x.ltl_Groups).Count());
            //}
        }
    }
}
