using System.Collections.Generic;
using iTextSharp.text;
using LP.ServiceHost.DataContracts.Common.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class GivenWritingToAPdf : BaseGiven
    {
        private Document _document;
  
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenDataPdfHasAFilterMessageAndThereAreNoGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf { FilterMessage = "dfdfdf", GlossaryTitleStr = "GlossaryTitleStr" };

                _document = SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.IsNotNull(_document);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsCalledTheCorrectAmountOfTimes()
            {
                const int expected = 1;

                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            }

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledOnceForTheGlossaryTitle()
            //{
            //    DocumentMock.Verify(m => m.Add(It.Is<Paragraph>(x => x.Content == "GlossaryTitleStr" && x.Font == TitleBlueFont && x.Alignment == Element.ALIGN_CENTER)), Times.Once());
            //}
        }

        public class WhenDataPdfHasAFilterMessageAndThereAreSomeGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf
                {
                    FilterMessage = "dfdfdf",
                    GlossaryTitleStr = "GlossaryTitleStr",
                    GlossaryItems = new List<GlossaryItem>
                {
                    new GlossaryItem(), new GlossaryItem(), new GlossaryItem()
                }
                };

                _document = SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.IsNotNull(_document);
            }

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledTheCorrectAmountOfTimes()
            //{
            //    const int expected = 2;

            //    DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            //}

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledOnceForTheGlossaryTitle()
            //{
            //    DocumentMock.Verify(m => m.Add(It.Is<Paragraph>(x => x.Content == "GlossaryTitleStr" && x.Font == TitleBlueFont && x.Alignment == Element.ALIGN_CENTER)), Times.Once());
            //}
        }

        public class WhenDataPdfHasNoFilterMessageAndThereAreNoGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf { GlossaryTitleStr = "GlossaryTitleStr" };

                _document = SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.IsNotNull(_document);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsCalledTheCorrectAmountOfTimes()
            {
                const int expected = 1;

                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            }

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledOnceForTheGlossaryTitle()
            //{
            //    DocumentMock.Verify(m => m.Add(It.Is<Paragraph>(x => x.Content == "GlossaryTitleStr" && x.Font == TitleBlueFont && x.Alignment == Element.ALIGN_CENTER)), Times.Once());
            //}
        }

        public class WhenDataPdfHasNoFilterMessageAndThereAreSomeGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf
                {
                    GlossaryTitleStr = "GlossaryTitleStr",
                    GlossaryItems = new List<GlossaryItem>
                {
                    new GlossaryItem(), new GlossaryItem(), new GlossaryItem()
                }
                };

                _document = SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenDocumentReturnedIsNotNull()
            {
                Assert.IsNotNull(_document);
            }

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledTheCorrectAmountOfTimes()
            //{
            //    const int expected = 1;

            //    DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            //}

            //[Test]
            //public void ThenAddParagraphToDocumentIsCalledOnceForTheGlossaryTitle()
            //{
            //    DocumentMock.Verify(m => m.Add(It.Is<Paragraph>(x => x.Content == "GlossaryTitleStr" && x.Font == TitleBlueFont && x.Alignment == Element.ALIGN_CENTER)), Times.Once());
            //}
        }

        public class WhenDataPdfHasNoGlossaryTitleAndHasFilterMessageButNoGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf()
                {
                    FilterMessage = "FilterMessage"
                };

                SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsCalledTheCorrectAmmountOfTimes()
            {
                const int expected = 1;
                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            }
        }

        public class WhenDataPdfHasAGlossaryTitleAndHasFilterMessageButNoGlossaryItems : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf()
                {
                    GlossaryTitleStr = "GlossaryTitleStr",
                    FilterMessage = "FilterMessage"
                };

                SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsCalledTheCorrectAmmountOfTimes()
            {
                const int expected = 1;
                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Exactly(expected));
            }
        }

        public class WhenDataPdfIsNotNull : GivenWritingToAPdf
        {
            protected override void When()
            {
                var dataPdf = new DataPdf()
                {
                    GlossaryTitleStr = "GlossaryTitleStr",
                    FilterMessage = "FilterMessage"
                };
                SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, dataPdf);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsCallesAtLeastOnce()
            {
                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.AtLeastOnce);
            }
        }

        public class WhenDataPdfIsNull : GivenWritingToAPdf
        {
            protected override void When()
            {
                SUT.WriteIntoPdfDocument(WriterMock.Object, DocumentMock.Object, null);
            }

            [Test]
            public void ThenAddParagraphToDocumentIsNeverCalled()
            {
                DocumentMock.Verify(m => m.Add(It.IsAny<Paragraph>()), Times.Never);
            }
        }
    }
}
