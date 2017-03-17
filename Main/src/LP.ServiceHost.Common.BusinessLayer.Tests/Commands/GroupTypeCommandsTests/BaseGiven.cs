using System.Collections.Generic;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupTypeCommandsTests
{
    public class BaseGiven : SpecsFor<GroupTypeCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<ITrainingAreaCommands> TrainingAreaCommandsMock = new Mock<ITrainingAreaCommands>();

        protected static ltl_GroupType GroupType1 = new ltl_GroupType {ID = 1};
        protected static ltl_GroupType GroupType2 = new ltl_GroupType {ID = 2};
        protected static ltl_GroupType GroupType3 = new ltl_GroupType {ID = 3};
        protected static ltl_GroupType GroupType4 = new ltl_GroupType {ID = 4};
        protected static ltl_GroupType GroupType5 = new ltl_GroupType {ID = 5};
        protected static ltl_GroupType GroupType6 = new ltl_GroupType {ID = 6};
        protected static ltl_GroupType GroupType7 = new ltl_GroupType {ID = 7};
        protected static ltl_GroupType GroupType8 = new ltl_GroupType {ID = 8};
        protected static ltl_GroupType GroupType9 = new ltl_GroupType {ID = 9};
        protected static ltl_GroupType GroupType10 = new ltl_GroupType {ID = 10};

        protected List<TrainingArea> TrainingAreas = new List<TrainingArea>
        {
            new TrainingArea
            {
                TrainingAreaID = 1,
                ltl_Groups = new List<Group>
                {
                    new Group {GroupID = 1, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType1},
                    new Group {GroupID = 2, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType2},
                    new Group {GroupID = 3, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType2},
                    new Group {GroupID = 4, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType3},
                    new Group {GroupID = 5, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType4},
                    new Group {GroupID = 6, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType4},
                    new Group {GroupID = 7, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType4},
                    new Group {GroupID = 8, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType5},
                    new Group {GroupID = 9, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType6},
                    new Group {GroupID = 10, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType6}
                }
            },
            new TrainingArea
            {
                TrainingAreaID = 2,
                ltl_Groups = new List<Group>
                {
                    new Group {GroupID = 11, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType1},
                    new Group {GroupID = 12, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType7},
                    new Group {GroupID = 13, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType7},
                    new Group {GroupID = 14, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType8},
                    new Group {GroupID = 15, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType8},
                    new Group {GroupID = 16, StatusBankID = (int) Status.Live, ltl_GroupType = GroupType8},
                    new Group
                    {
                        GroupID = 17,
                        StatusBankID = (int) Status.TranslationInProgress,
                        ltl_GroupType = GroupType9
                    },
                    new Group {GroupID = 18, StatusBankID = (int) Status.Deleted, ltl_GroupType = GroupType9},
                    new Group {GroupID = 19, StatusBankID = (int) Status.ComingSoon, ltl_GroupType = GroupType10},
                    new Group
                    {
                        GroupID = 20,
                        StatusBankID = (int) Status.TranslationInProgress,
                        ltl_GroupType = GroupType10
                    }
                }
            },
            new TrainingArea {TrainingAreaID = 3, ltl_Groups = new List<Group>()}
        };





        protected void PrepareSut()
        {
            TrainingAreaCommandsMock.Setup(m => m.GetLiveTrainingAreasWithIncludes()).ReturnsAsync(TrainingAreas);

            SUT = new GroupTypeCommands(BaseCommandsMock.Object, TrainingAreaCommandsMock.Object);
        }
    }
}
