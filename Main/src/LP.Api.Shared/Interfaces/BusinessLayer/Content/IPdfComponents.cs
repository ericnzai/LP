using iTextSharp.text.pdf;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IPdfComponents
    {
        PdfPTable CreatePdfPTable(int numberOfColumns, float totalWidth);
    }
}
