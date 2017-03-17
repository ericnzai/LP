using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Request.Translation;

namespace LP.Translation.Tests.ControllerTests.StaticTranslationControllerTests
{
    public class GivenPostingToTheController : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenSomeTranslationsShouldBeReturned : GivenPostingToTheController
        {
            protected override async void When()
            {
                var x = await SUT.Post(new TranslationRequestContract());
            }
        }
    }
}
