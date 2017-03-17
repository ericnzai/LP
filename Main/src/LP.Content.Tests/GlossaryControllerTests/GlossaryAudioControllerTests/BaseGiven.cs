using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.Controllers;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryAudioControllerTests
{
    public class BaseGiven : SpecsFor<GlossaryAudioController>
    {
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<IGlossaryCommands> GlossaryCommandsMock = new Mock<IGlossaryCommands>();

        protected GlossaryAudioResponseContract ExistingEnabledGlossaryAudioResponseContract = new GlossaryAudioResponseContract
        {
            IsEnabled = true,
            FileName = "ExistingEnabledGlossary.mp3",
            AudioBase64 = "ThisIsTheExistingEnabledGlossary"
        };

        protected GlossaryAudioResponseContract ExistingDisabledGlossaryAudioResponseContract = new GlossaryAudioResponseContract
        {
            IsEnabled = false,
            FileName = "ExistingDisabledGlossary.mp3",
            AudioBase64 = "ThisIsTheExistingDisabledGlossary"
        };
        protected const int ExistingEnabledGlossaryAudioId = 1;
        protected const int ExistingDisabledGlossaryAudioId = 2;
        protected const int NonExistantGlossaryAudioId = 3;
        protected readonly Mock<HttpRequestMessage> HttpRequestMessageMock = new Mock<HttpRequestMessage>();
        protected void PrepareSut()
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/glossary-audio/");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "glossaryaudio" } });

            GlossaryCommandsMock.Setup(m => m.GetGlossaryAudio(It.Is<int>(glossaryId => glossaryId == ExistingEnabledGlossaryAudioId)))
                .ReturnsAsync(ExistingEnabledGlossaryAudioResponseContract);

            GlossaryCommandsMock.Setup(m => m.GetGlossaryAudio(It.Is<int>(glossaryId => glossaryId == ExistingDisabledGlossaryAudioId)))
                .ReturnsAsync(ExistingDisabledGlossaryAudioResponseContract);

            GlossaryCommandsMock.Setup(m => m.GetGlossaryAudio(It.Is<int>(glossaryId => glossaryId == NonExistantGlossaryAudioId)))
                .ReturnsAsync(null);

            AskContentApiBusinessMock.Setup(m => m.GlossaryCommands).Returns(GlossaryCommandsMock.Object);

            SUT = new GlossaryAudioController(AskContentApiBusinessMock.Object)
            {
                
                Configuration = new HttpConfiguration(),
            };

            SUT.Request = new HttpRequestMessage();
            SUT.Request.SetConfiguration(new HttpConfiguration());
        }
    }
}
