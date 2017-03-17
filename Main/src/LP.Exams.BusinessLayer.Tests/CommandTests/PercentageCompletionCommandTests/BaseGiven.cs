using System.Collections.Generic;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Exams.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.PercentageCompletionCommandTests
{
    public class BaseGiven : SpecsFor<PercentageCompletionCommands>
    {
        protected readonly Mock<IExamCommands> ExamCommandsMock = new Mock<IExamCommands>();
        protected readonly Mock<IAttemptsCommands> AttemptsCommandsMock = new Mock<IAttemptsCommands>();
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<ICommonCalculatorCommands> CommonCalculatorCommandsMock = new Mock<ICommonCalculatorCommands>();
        protected const int GroupId1 = 56;
        protected const string GroupName1 = "Test Group 1";
        protected const int GroupId2 = 58;
        protected const string GroupName2 = "Test Group 2";
        protected List<short> ExamIdsForGroup1 = new List<short>{1,2,3,4,5};
        protected List<short> ExamIdsForGroup2 = new List<short>{6,7,8, 9,10,11,12,13,14,15};
        protected const int UserId = 108;
        protected List<short> PassedExamIdsForUser = new List<short>{1,3,5};
        protected List<short> PassedExamIdsForUser2 = new List<short>{7,8,9,10,11,12, 15};
        protected Group Group1 = new Group { GroupID = GroupId1, Name = GroupName1 };
        protected Group Group2 = new Group { GroupID = GroupId2, Name = GroupName2 };

        protected void PrepareSut()
        {
            ExamCommandsMock.Setup(m => m.GetExamIdsForGroup(It.Is<int>(x => x == GroupId1), It.IsAny<bool>()))
                .ReturnsAsync(ExamIdsForGroup1);

            ExamCommandsMock.Setup(m => m.GetExamIdsForGroup(It.Is<int>(x => x == GroupId2), It.IsAny<bool>()))
                .ReturnsAsync(ExamIdsForGroup2);
            
            AttemptsCommandsMock.Setup(m => m.GetPassedAttemptsForUser(It.Is<int>(x => x == UserId), It.Is<IEnumerable<short>>(x =>
                Equals(x, ExamIdsForGroup1))))
                .ReturnsAsync(PassedExamIdsForUser);

            AttemptsCommandsMock.Setup(m => m.GetPassedAttemptsForUser(It.Is<int>(x => x == UserId), It.Is<IEnumerable<short>>(x =>
                Equals(x, ExamIdsForGroup2))))
                .ReturnsAsync(PassedExamIdsForUser2);

            
            BaseCommandsMock.Setup(m => m.GetByIdAsync<Group>(It.Is<int>(x => x == GroupId1))).ReturnsAsync(Group1);
            BaseCommandsMock.Setup(m => m.GetByIdAsync<Group>(It.Is<int>(x => x == GroupId2))).ReturnsAsync(Group2);

               
            CommonCalculatorCommandsMock.Setup(m => m.CalculatePercentages(It.IsAny<int>(), It.Is<int>(x => x == 5))).Returns(60);

            CommonCalculatorCommandsMock.Setup(m => m.CalculatePercentages(It.IsAny<int>(), It.Is<int>(x => x == 10))).Returns(70);

            SUT = new PercentageCompletionCommands(ExamCommandsMock.Object, AttemptsCommandsMock.Object, BaseCommandsMock.Object, CommonCalculatorCommandsMock.Object);
        }
    }
}
