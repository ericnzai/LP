using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Commands;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.NumberAchievedCommandsTests
{
    public class BaseGiven : SpecsFor<NumberAchievedCommands>
    {
        protected readonly Mock<IGroupPermissionCommands> GroupPermissionCommandsMock = new Mock<IGroupPermissionCommands>();
        protected readonly Mock<ITrainingAreaCommands> TrainingAreaCommandsMock = new Mock<ITrainingAreaCommands>();
        protected readonly Mock<IPercentageCompletionCommands> PercentageCompletionCommandsMock = new Mock<IPercentageCompletionCommands>();

        protected List<TrainingArea> TrainingAreas = new List<TrainingArea>
        {
            new TrainingArea{TrainingAreaID = 1, Name = "Training Area 1"},
            new TrainingArea{TrainingAreaID = 2, Name = "Training Area 2"},
            new TrainingArea{TrainingAreaID = 3, Name = "Training Area 3"},
        };

        protected readonly IEnumerable<int> FirstTrainingAreaGroupIds = new List<int>
        {
            1,
            2,
            3,
            4,
            5,
            11,
            12,
            13,
            14,
            15,
            21,
            22,
            23,
            24,
            25
        };


        protected List<Group> Groups = new List<Group>
        {
          new Group {TrainingAreaID = 1, GroupID = 1, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 2, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 3, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 4, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 5, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 6, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 7, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 8, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 9, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 10, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 11, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 12, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 13, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 14, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 15, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 16, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 17, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 18, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 19, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 20, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 21, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 22, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 23, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 24, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 1, GroupID = 25, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 26, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 27, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 2, GroupID = 28, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 29, StatusBankID = (int)Status.Live},
          new Group {TrainingAreaID = 3, GroupID = 30, StatusBankID = (int)Status.Live},
        };

        protected List<GroupPercentageComplete> GroupPercentageCompletes = new List<GroupPercentageComplete>
        {
            new GroupPercentageComplete{}
        };

        protected List<GroupPercentageComplete> FirstGroupPercentageCompletes = new List<GroupPercentageComplete>
        {
            new GroupPercentageComplete{GroupId = 1, GroupName = "Group 1", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 2, GroupName = "Group 2", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 3, GroupName = "Group 3", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 4, GroupName = "Group 4", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 5, GroupName = "Group 5", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 14, GroupName = "Group 5", PercentageComplete = 0}
        };

        protected List<GroupPercentageComplete> SecondGroupPercentageCompletes = new List<GroupPercentageComplete>
        {
            new GroupPercentageComplete{GroupId = 6, GroupName = "Group 6", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 7, GroupName = "Group 7", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 8, GroupName = "Group 8", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 16, GroupName = "Group 16", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 17, GroupName = "Group 17", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 18, GroupName = "Group 18", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 27, GroupName = "Group 27", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 28, GroupName = "Group 28", PercentageComplete = 10}
        };

        protected List<GroupPercentageComplete> ThirdGroupPercentageCompletes = new List<GroupPercentageComplete>
        {
            new GroupPercentageComplete{GroupId = 9, GroupName = "Group 9", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 10, GroupName = "Group 10", PercentageComplete = 100},
            new GroupPercentageComplete{GroupId = 30, GroupName = "Group 30", PercentageComplete = 8}
        };

        protected void PrepareSut()
        {
            TrainingAreaCommandsMock.Setup(m => m.GetLiveTrainingAreas()).ReturnsAsync(TrainingAreas.AsQueryable());

            GroupPermissionCommandsMock.Setup(
                m =>
                    m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 1),
                        It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(Groups.Where(t => t.TrainingAreaID == 1));

            GroupPermissionCommandsMock.Setup(
               m =>
                   m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 2),
                       It.IsAny<IEnumerable<int>>()))
               .ReturnsAsync(Groups.Where(t => t.TrainingAreaID == 2));

            GroupPermissionCommandsMock.Setup(
               m =>
                   m.GroupsWithPermissionsForRolesForTrainingArea(It.IsAny<IEnumerable<int>>(), It.Is<int>(x => x == 3),
                       It.IsAny<IEnumerable<int>>()))
               .ReturnsAsync(Groups.Where(t => t.TrainingAreaID == 3));

            PercentageCompletionCommandsMock.Setup(
                m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(GroupPercentageCompletes);

            PercentageCompletionCommandsMock.Setup(
                m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.Is<IEnumerable<int>>(x => x.Contains(1))))
                .ReturnsAsync(FirstGroupPercentageCompletes);

            PercentageCompletionCommandsMock.Setup(
                 m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.Is<IEnumerable<int>>(x => x.Contains(6))))
                 .ReturnsAsync(SecondGroupPercentageCompletes);

            PercentageCompletionCommandsMock.Setup(
                           m => m.PercentageAchievedForGroups(It.IsAny<int>(), It.Is<IEnumerable<int>>(x => x.Contains(9))))
                           .ReturnsAsync(ThirdGroupPercentageCompletes);

            SUT = new NumberAchievedCommands(GroupPermissionCommandsMock.Object, TrainingAreaCommandsMock.Object,
                 PercentageCompletionCommandsMock.Object);
        }
    }
}
