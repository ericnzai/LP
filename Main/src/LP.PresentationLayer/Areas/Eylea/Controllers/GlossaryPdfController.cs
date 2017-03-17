using System.Collections.Generic;
using System.Threading.Tasks;
using LP.Api.Shared.Providers;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class GlossaryPdfController : BaseController
    {
        // GET: Eylea/GlossaryPdf
        public async Task<string> DownloadGlossary()
        {
            var translatedItems = await GetTranslations();
            var requestGlossaryPdf = new GlossaryPdfRequestContract(){TranslatedItems = translatedItems};
            var glossaryPdfResponseContract =
                await PostRequestToService<GlossaryPdfRequestContract, GlossaryPDFResponseContract>("api/content/glossary-pdf", requestGlossaryPdf);

            return glossaryPdfResponseContract.Content;
        }

        // GET: Eylea/GlossaryPdf
        public async Task<string> DownloadFilteredGlossary(string filters, string sort)
        {
            var translatedItems = await GetTranslations();
            var requestGlossaryPdf = new GlossaryFilteredPdfRequestContract() { TranslatedItems = translatedItems, Filters = filters, Sort = sort};
            var glossaryPdfResponseContract =
                await PostRequestToService<GlossaryFilteredPdfRequestContract, GlossaryPDFResponseContract>("api/content/glossary-filtered-pdf", requestGlossaryPdf);

            return glossaryPdfResponseContract.Content;
        }

        public async Task<List<TranslatedItem>> GetTranslations()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest
                {
                    ResourceId = "ltGlossary.Text",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslationRequest
                {
                    ResourceId = "msgSorryNoResults",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslationRequest
                {
                    ResourceId = "msgFollowingFiltersSelected",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslationRequest
                {
                    ResourceId = "msgFollowingFiltersSelectedForSearch",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslationRequest
                {
                    ResourceId = "msgFollowingFiltersSelectedForTrainingModules",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                }
            });

            return translatedItems;
        }
    }
}