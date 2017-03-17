using iTextSharp.text.pdf;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;

namespace LP.Content.BusinessLayer.PdfCreation
{
    public class PdfComponents : IPdfComponents
    {
        public PdfPTable CreatePdfPTable(int numberOfColumns, float totalWidth)
        {
            return new PdfPTable(numberOfColumns) { TotalWidth = totalWidth };
        }
    }
}
