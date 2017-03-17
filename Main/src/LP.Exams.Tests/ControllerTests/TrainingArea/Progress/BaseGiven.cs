using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Exams.Controllers.TrainingArea;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using SpecsFor;

namespace LP.Exams.Tests.ControllerTests.TrainingArea.Progress
{
    public class BaseGiven : SpecsFor<ProgressController>
    {
        protected readonly Mock<IAskExamsApiBusiness>AskExamsApiBusinessMock = new Mock<IAskExamsApiBusiness>();
        protected readonly Mock<IGroupCompletionCommands> GroupCompletionCommandsMock = new Mock<IGroupCompletionCommands>();
        protected TrainingAreaProgressResponseContract TrainingAreaProgressResponseContract = new TrainingAreaProgressResponseContract();
        protected void PrepareSut()
        {
            GroupCompletionCommandsMock.Setup(
                m => m.GetAllCompleteGroupsForTrainingAreaAsync(It.IsAny<int>(), It.IsAny<UserDetails>()))
                .ReturnsAsync(TrainingAreaProgressResponseContract);

            AskExamsApiBusinessMock.Setup(m => m.GroupCompletionCommands).Returns(GroupCompletionCommandsMock.Object);
            
            SUT = new ProgressController(AskExamsApiBusinessMock.Object);
        }
    }
}
