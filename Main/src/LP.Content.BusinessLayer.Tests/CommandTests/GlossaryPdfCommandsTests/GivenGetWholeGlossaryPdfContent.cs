﻿using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Response.Content;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class GivenGetWholeGlossaryPdfContent : BaseGiven
    {
        private GlossaryPDFResponseContract _response;
        protected override void Given()
        {
            PrepareSut();

        }

        public class WhenCultureAndTranslationsAreNotNull : GivenGetWholeGlossaryPdfContent
        {
            protected override async void When()
            {
                _response = await SUT.GetWholeGlossaryPdfContent("en", new List<TranslatedItem>());
            }

            [Test]
            public void ThenResponseIsNotNull()
            {
                Assert.IsNotNull(_response);
            }

            [Test]
            public void ThenResponseContentIsNotNull()
            {
                Assert.IsNotNull(_response.Content);
            }
        }
    }
}
