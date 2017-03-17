using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Tests.Helpers;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.GroupCompletionCommandsTests
{
    public class GivenGettingAllCompleteGroupsForTrainingArea : BaseGiven
    {
        private TrainingAreaProgressResponseContract _trainingAreaProgressResponseContract;
        private const int TrainingAreaId = 56;
        private const string TrainingAreaName = "Training Area 56";
        private ltl_GroupType _groupType1;
        private ltl_GroupType _groupType2;
        private ltl_GroupType _groupType3;
        private ltl_GroupType _groupType4;
        private ltl_GroupType _groupType5;
        private Group _group1;
        private Group _group2;
        private Group _group3;
        private Group _group4;
        private Group _group5;
        protected override void Given()
        {
            var trainingArea = new TrainingArea { TrainingAreaID = TrainingAreaId, Name = TrainingAreaName };
            _groupType1 = new ltl_GroupType{ID = 5};
            _groupType2 = new ltl_GroupType{ID = 6};
            _groupType3 = new ltl_GroupType { ID = 7 };
            _groupType4 = new ltl_GroupType { ID = 8 };
            _groupType5 = new ltl_GroupType { ID = 9 };

            _group1 = new Group {Culture = DefaultGlobalCulture, GroupID = 10, StatusBankID = (int)Status.Live};
            _group2 = new Group { Culture = DefaultGlobalCulture, GroupID = 11, StatusBankID = (int)Status.Live };
            _group3 = new Group { Culture = DefaultGlobalCulture, GroupID = 12, StatusBankID = (int)Status.Live };
            _group4 = new Group { Culture = DefaultGlobalCulture, GroupID = 13, StatusBankID = (int)Status.Live };
            _group5 = new Group { Culture = DefaultGlobalCulture, GroupID = 14, StatusBankID = (int)Status.Live };

            var groups1 = new List<Group>
            {
                _group1,
                _group2,
                _group3
            };

            var groups2 = new List<Group>
            {
                _group4,
                _group5
            };
            
            var groupsGroupedByGroupType = new List<Grouping<ltl_GroupType, Group>>
            {
                new Grouping<ltl_GroupType, Group>(_groupType1, groups1 ),
                new Grouping<ltl_GroupType, Group>(_groupType2, groups2 ),
                new Grouping<ltl_GroupType, Group>(_groupType3, groups2 ),
                new Grouping<ltl_GroupType, Group>(_groupType4, groups2 ),
                new Grouping<ltl_GroupType, Group>(_groupType5, groups2 )
            };

            TrainingAreaGroupedByGroupType = new Grouping<TrainingArea, IGrouping<ltl_GroupType, Group>>(trainingArea, groupsGroupedByGroupType);

            TrainingsExams = new List<TrainingsExam>
            {
                new TrainingsExam {GroupId = 10, IsLive = true, SectionId = 1, ParentSectionId = 100},
                new TrainingsExam {GroupId = 10, IsLive = true, SectionId = 2, ParentSectionId = 101},
                new TrainingsExam {GroupId = 10, IsLive = true, SectionId = 3, ParentSectionId = 102},
                new TrainingsExam {GroupId = 11, IsLive = true, SectionId = 1},
                new TrainingsExam {GroupId = 11, IsLive = true, SectionId = 2},
                new TrainingsExam {GroupId = 11, IsLive = true, SectionId = 3},
                new TrainingsExam {GroupId = 12, IsLive = true, SectionId = 1},
                new TrainingsExam {GroupId = 12, IsLive = true, SectionId = 2},
                new TrainingsExam {GroupId = 12, IsLive = true, SectionId = 3},
                new TrainingsExam {GroupId = 13, IsLive = true, SectionId = 1},
                new TrainingsExam {GroupId = 13, IsLive = true, SectionId = 2},
                new TrainingsExam {GroupId = 13, IsLive = true, SectionId = 3},
                new TrainingsExam {GroupId = 14, IsLive = true, SectionId = 1},
                new TrainingsExam {GroupId = 14, IsLive = true, SectionId = 2},
                new TrainingsExam {GroupId = 14, IsLive = true, SectionId = 3},
            };

            GroupsPercentageComplete = new List<GroupPercentageComplete>
            {
                new GroupPercentageComplete{GroupId = 5, GroupName = "Group 5", PercentageComplete = 30},
                new GroupPercentageComplete{GroupId = 6, GroupName = "Group 6", PercentageComplete = 50},
                new GroupPercentageComplete{GroupId = 10, GroupName = "Group 10", PercentageComplete = 20},
                new GroupPercentageComplete{GroupId = 11, GroupName = "Group 11", PercentageComplete = 40},
                new GroupPercentageComplete{GroupId = 12, GroupName = "Group 12", PercentageComplete = 60},
                new GroupPercentageComplete{GroupId = 13, GroupName = "Group 13", PercentageComplete = 80},
                new GroupPercentageComplete{GroupId = 14, GroupName = "Group 14", PercentageComplete = 100}
            };

            PrepareSut();
        }

        public class WhenGettingAllCompleteGroupsForTrainingAreaAndCheckingDependenciesAreCalledCorrectly : GivenGettingAllCompleteGroupsForTrainingArea
        {
            protected override async void When()
            {
                _trainingAreaProgressResponseContract = await SUT.GetAllCompleteGroupsForTrainingAreaAsync(TrainingAreaId, UserDetails);
            }

            [Test]
            public void ThenTrainingAreaCommandsGetTrainingAreaWithAllGroupInfoIsCalledOnce()
            {
                TrainingAreaCommandsMock.Verify(m =>
                        m.GetTrainingAreaWithAllGroupInfo(It.IsAny<int>(), It.IsAny<List<int>>(), It.IsAny<AccessType>()), Times.Once());
            }

            [Test]
            public void ThenTrainingAreaCommandsGetTrainingAreaWithAllGroupInfoIsCalledOnceWithTheCorrectParameters()
            {
                TrainingAreaCommandsMock.Verify(m =>
                        m.GetTrainingAreaWithAllGroupInfo(It.Is<int>(x => x == TrainingAreaId), It.Is<List<int>>(x => x == UserDetails.RoleIds), It.Is<AccessType>(x => x == AccessType.DisplayOnDashboards)), Times.Once());
            }

            [Test]
            public void ThenPercentageCompletionCommandsPercentageAchievedForGroupsIsCalledOnce()
            {
                PercentageCompletionCommandsMock.Verify(m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenPercentageCompletionCommandsPercentageAchievedForGroupsIsCalledOnceWithTheCorrectParameters()
            {
                PercentageCompletionCommandsMock.Verify(m => m.PercentageAchievedForGroups(It.Is<int>(x => x == UserDetails.UserId), It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenLastAreasViewedProviderGetLastSectionsViewedIsCalledOnce()
            {
                LastAreasViewedProviderMock.Verify(m => m.GetLastSectionsViewed(It.IsAny<int>(), It.IsAny<IEnumerable<Group>>()), Times.Once());   
            }

            [Test]
            public void ThenLastAreasViewedProviderGetLastSectionsViewedIsCalledOnceWithTheCorrectParameters()
            {
                LastAreasViewedProviderMock.Verify(m => m.GetLastSectionsViewed(It.Is<int>(x => x == UserDetails.UserId), It.IsAny<IEnumerable<Group>>()), Times.Once());
            }

            [Test]
            public void ThenCultureProviderGetCultureInfoWithDefaultIsCalledTheCorrectAmountOfTimes()
            {
                const int expectedTimes = 5;

                CultureProviderMock.Verify(m => m.GetCultureInfoWithDefault(It.IsAny<string>()), Times.Exactly(expectedTimes));   
            }

            [Test]
            public void ThenCultureProviderGetCultureInfoWithDefaultIsCalledTheCorrectAmountOfTimesWithTheCorrectParameters()
            {
                const int expectedTimes = 5;

                CultureProviderMock.Verify(m => m.GetCultureInfoWithDefault(It.Is<string>(x => x == DefaultGlobalCulture)), Times.Exactly(expectedTimes));
            }


           
        }

        public class WhenGettingAllCompleteGroupsForTrainingAreaAndCheckingTheCorrectTrainingAreaIsReturned :
            GivenGettingAllCompleteGroupsForTrainingArea
        {
            protected override async void When()
            {
                _trainingAreaProgressResponseContract =
                    await SUT.GetAllCompleteGroupsForTrainingAreaAsync(TrainingAreaId, UserDetails);
            }


            [Test]
            public void ThenTrainingAreaProgressResponseContractIsNotNull()
            {
                Assert.IsNotNull(_trainingAreaProgressResponseContract);
            }

            [Test]
            public void ThenTrainingAreaReturnedIdIsCorrect()
            {
                Assert.AreEqual(TrainingAreaId, _trainingAreaProgressResponseContract.TrainingAreaId);
            }

            [Test]
            public void ThenTrainingAreaReturnedNameIsCorrect()
            {
                Assert.AreEqual(TrainingAreaName, _trainingAreaProgressResponseContract.TrainingAreaName);
            }
        }

        public class WhenGettingAllCompleteGroupsForTrainingAreaAndCheckingTheCorrectGroupProgressContractsReturned :
            GivenGettingAllCompleteGroupsForTrainingArea
        {
            private GroupProgressContract _firstGroupProgressContract;
            private GroupProgressContract _secondGroupProgressContract;

            protected override async void When()
            {
                _trainingAreaProgressResponseContract =
                    await SUT.GetAllCompleteGroupsForTrainingAreaAsync(TrainingAreaId, UserDetails);

                _firstGroupProgressContract = _trainingAreaProgressResponseContract.GroupProgressContracts.First();

                _secondGroupProgressContract = _trainingAreaProgressResponseContract.GroupProgressContracts.Last();
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupProgressContractsAreReturned()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _trainingAreaProgressResponseContract.GroupProgressContracts.Count);
            }

            [Test]
            public void ThenFirstGroupProgressContractIsNotNull()
            {
                Assert.IsNotNull(_firstGroupProgressContract);
            }

            [Test]
            public void ThenSecondGroupProgressContractIsNotNull()
            {
                Assert.IsNotNull(_secondGroupProgressContract);
            }

            //[Test]
            //public void ThenTheFirstGroupProgressContractIsCorrect()
            //{
            //    Assert.AreEqual(3434, _trainingAreaProgressResponseContract.GroupProgressContracts.First().GroupId);
            //}
        }
    }
}
