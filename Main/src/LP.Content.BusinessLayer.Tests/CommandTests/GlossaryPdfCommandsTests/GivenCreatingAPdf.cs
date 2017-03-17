using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class GivenCreatingAPdf : BaseGiven
    {
        private string _content;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenDataPdfHasAFilterMessageAndThereAreNoGlossaryItems : GivenCreatingAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf { FilterMessage = "dfdfdf", GlossaryTitleStr = "GlossaryTitleStr" };

                _content = SUT.CreatePdf(dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.False(string.IsNullOrEmpty(_content));
            }
        }

        public class WhenDataPdfHasAFilterMessageAndThereAreGlossaryItems : GivenCreatingAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf
                {
                    FilterMessage = "dfdfdf",
                    GlossaryTitleStr = "GlossaryTitleStr",
                    GlossaryItems = new List<GlossaryItem>()
                    {
                        new GlossaryItem(){Title = "Glossary 1", Description = "Description glossary 1"},
                        new GlossaryItem(){Title = "Glossary 2", Description = "Description glossary 2"},
                        new GlossaryItem(){Title = "Glossary 3", Description = "Description glossary 3"},
                    }
                };

                _content = SUT.CreatePdf(dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.False(string.IsNullOrEmpty(_content));
            }
        }
    }
}
