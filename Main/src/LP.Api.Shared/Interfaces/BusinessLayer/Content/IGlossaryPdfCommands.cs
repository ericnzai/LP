using System.Collections.Generic;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IGlossaryPdfCommands
    {
        Task<GlossaryPDFResponseContract> GetWholeGlossaryPdfContent(string culture, List<TranslatedItem> translatedItems);
        Task<GlossaryPDFResponseContract> GetFilteredGlossaryPdfContent(string culture, List<TranslatedItem> translatedItems, string filters, string sort);
        string BuildFilterMessage(string filters, List<TranslatedItem> translatedItems);

        string BuildFilterMessage(string filters, string msgFollowingFiltersSelectedStr,
            string msgFollowingFiltersSelectedForSearchStr);

        string CreatePdf(DataPdf dataPdf);
        Document WriteIntoPdfDocument(PdfWriter writer, Document doc, DataPdf dataPdf);
    }
}
