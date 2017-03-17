using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Moq;

namespace LP.Content.BusinessLayer.Tests.FakeObjects
{
    public class FakePdfContentByte:PdfContentByte
    {
        private PdfWriter _pdfWriter;
        private Mock<Document> _document;

        public FakePdfContentByte(PdfWriter wr) : base(wr)
        {
            var doc = new Document(PageSize.A4, 36, 36, 54, 56);

            if (wr == null)
            {
                wr = PdfWriter.GetInstance(doc, new MemoryStream());
            }

            _pdfWriter = wr;
        }

        public PdfContentByte GetInstance()
        {
            return new FakePdfContentByte(_pdfWriter);
        }
    }
}
