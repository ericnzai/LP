using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Mime;
using LP.Model.ViewModels.Glossary;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class GlossaryController : BaseController
    {
        // GET: Eylea/Glossary
        public async Task<JsonResult> GlossaryItems()
        {
            var glossaryItemResponseContract =
                await GetResponseFromService<GlossaryItemsResponseContract>("api/content/glossary-item", null);

            var glossaryItemsViewModel = new GlossaryItemsViewModel()
            {
                GlossaryItems = glossaryItemResponseContract.GlossaryItems
            };
            
            return Json(glossaryItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        public async Task<PartialViewResult> GlossaryTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest
                {
                    ResourceId = "ltGlossary.Text",
                    ResourceSet = "Master/Master.aspx"
                }
            });

            var translatedItem = translatedItems.FirstOrDefault();

            return PartialView("~/Views/Shared/Partial/_SingleTranslatedItem.cshtml", new TranslatedItem() { TranslatedValue = translatedItem.TranslatedValue, ResourceId = translatedItem.ResourceId, ResourceSet = translatedItem.ResourceSet});
        }

        public async Task<JsonResult> StreamAudio(int id)
        {
            var url = string.Format("api/content/glossary-audio/{0}", id);

            var glossaryAudioResponseContract =
                await GetResponseFromService<GlossaryAudioResponseContract>(url, null);

            return Json(glossaryAudioResponseContract.AudioBase64, JsonRequestBehavior.AllowGet);
        }
    }
}