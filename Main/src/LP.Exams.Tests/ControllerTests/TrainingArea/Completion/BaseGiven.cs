using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Exams.Controllers.TrainingArea;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using SpecsFor;

namespace LP.Exams.Tests.ControllerTests.TrainingArea.Completion
{
    public class BaseGiven : SpecsFor<CompletionController>
    {
        protected readonly Mock<IAskExamsApiBusiness> AskExamsApiBusinessMock = new Mock<IAskExamsApiBusiness>();
        protected readonly Mock<INumberAchievedCommands> NumberAchievedCommandsMock = new Mock<INumberAchievedCommands>();
        protected Mock<HttpConfiguration> HttpConfigurationMock = new Mock<HttpConfiguration>();
        protected List<TrainingAreaCompletion> TrainingAreaCompletions = new List<TrainingAreaCompletion>();

        protected void PrepareSut()
        {
            NumberAchievedCommandsMock.Setup(
                m => m.NumberOfModulesCompletedForAllTrainingAreasAsync(It.IsAny<UserDetails>()))
                .ReturnsAsync(TrainingAreaCompletions);

            AskExamsApiBusinessMock.Setup(m => m.NumberAchievedCommands).Returns(NumberAchievedCommandsMock.Object);

            SUT = new CompletionController(AskExamsApiBusinessMock.Object)
            {
                ControllerContext = new HttpControllerContext { Configuration = HttpConfigurationMock.Object },
                Request = new HttpRequestMessage()
            };
            SUT.Request.SetConfiguration(HttpConfigurationMock.Object);

            //var controllerContext = new Mock<HttpControllerContext>();
            //var principal = new Moq.Mock<IPrincipal>();
            //principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            //principal.SetupGet(x => x.Identity.Name).Returns("test");
            //controllerContext.SetupGet(x => x.RequestContext.Principal).Returns(principal.Object);
            //SUT.ControllerContext = controllerContext.Object;
        }
    }
}
