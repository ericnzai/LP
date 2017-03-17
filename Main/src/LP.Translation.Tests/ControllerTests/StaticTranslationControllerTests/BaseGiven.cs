using LP.Api.Shared.Interfaces.BusinessLayer.Translation;
using LP.Translation.Controllers;
using Moq;
using SpecsFor;

namespace LP.Translation.Tests.ControllerTests.StaticTranslationControllerTests
{
    public class BaseGiven : SpecsFor<StaticTranslationController>
    {
        protected readonly Mock<IAskTranslationApiBusiness> AskTranslationApiBusinessMock = new Mock<IAskTranslationApiBusiness>();

        protected void PrepareSut()
        {
            SUT = new StaticTranslationController(AskTranslationApiBusinessMock.Object);
        }
    }
}
