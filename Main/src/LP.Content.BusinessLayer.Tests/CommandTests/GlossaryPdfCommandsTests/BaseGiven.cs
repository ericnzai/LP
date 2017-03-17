using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.BusinessLayer.Commands;
using LP.Content.BusinessLayer.Tests.FakeObjects;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class BaseGiven : SpecsFor<GlossaryPdfCommands>
    {
        protected readonly Mock<IGlossaryCommands> GlossaryCommandsMock = new Mock<IGlossaryCommands>();
        protected readonly Mock<IPdfCommands> PdfCommandsMock = new Mock<IPdfCommands>();
        protected readonly Mock<IConfigurationProvider> ConfigurationProviderMock = new Mock<IConfigurationProvider>();
        protected readonly Mock<IPdfComponents>  PdfComponentsMock = new Mock<IPdfComponents>();
        protected Mock<Document> DocumentMock = new Mock<Document>();
        protected Mock<PdfWriter> WriterMock = new Mock<PdfWriter>();
        protected Mock<PdfContentByte> ContentByteMock = new Mock<PdfContentByte>();

        protected Font TitleBlueFont = new Font(Font.FontFamily.COURIER);
        protected Font CoverBlueFont = new Font(Font.FontFamily.HELVETICA);
        protected Font ItalicGrayFont = new Font(Font.FontFamily.TIMES_ROMAN);
        protected Font GreyMediumFont = new Font(Font.FontFamily.TIMES_ROMAN);
        protected Font GreySmallFont = new Font(Font.FontFamily.TIMES_ROMAN);
        
        protected Mock<PdfPTable> PdfPTableMock = new Mock<PdfPTable>();
        protected Mock<Image> ImageMock = new Mock<Image>(new Uri("http://thisisfakeuri/"));

        protected void PrepareSut()
        {
            PdfPTableMock.Setup(
                m =>
                    m.WriteSelectedRows(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<float>(), It.IsAny<float>(),
                        It.IsAny<PdfContentByte>()));
            PdfComponentsMock.Setup(m => m.CreatePdfPTable(It.IsAny<int>(), It.IsAny<float>()))
                .Returns(PdfPTableMock.Object);

            var doc = new Document(PageSize.A4, 36, 36, 54, 56);
            //WriterMock.Setup(m => m.DirectContent).Returns(new FakePdfContentByte(PdfWriter.GetInstance(doc, new MemoryStream())));
            ConfigurationProviderMock.Setup(m => m.FrontEndWebUrl).Returns("http://thisisfakeuri/");
            PdfCommandsMock.Setup(m => m.TitleBlue).Returns(TitleBlueFont);
            PdfCommandsMock.Setup(m => m.CoverBlue).Returns(CoverBlueFont);
            PdfCommandsMock.Setup(m => m.ItalicGray).Returns(ItalicGrayFont);
            PdfCommandsMock.Setup(m => m.GreyMedium).Returns(GreyMediumFont);
            PdfCommandsMock.Setup(m => m.GreySmall).Returns(GreySmallFont);

            

            PdfCommandsMock.Setup(m => m.FrontCoverImage).Returns(ImageMock.Object);
            var pageSize = new Rectangle(100, 199);
            DocumentMock.Setup(m => m.PageSize).Returns(pageSize);

            GlossaryCommandsMock.Setup(m => m.GetAllGlossaryItems("en"))
                .ReturnsAsync(new GlossaryItemsResponseContract()
                {
                    GlossaryItems = new List<GlossaryItem>()
                    {
                        new GlossaryItem(){Title = "Glossary 1", Description = "Description glossary 1"},
                        new GlossaryItem(){Title = "Glossary 2", Description = "Description glossary 2"},
                        new GlossaryItem(){Title = "Glossary 3", Description = "Description glossary 3"},
                    }
                });

            SUT = new GlossaryPdfCommands(GlossaryCommandsMock.Object, PdfCommandsMock.Object, ConfigurationProviderMock.Object, PdfComponentsMock.Object);
        }
    }
}
