using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Api.Shared.Providers;
using LP.Model.Mappers;
using LP.Model.ViewModels.FeatureAttachments;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class FeatureAttachmentController : BaseController
    {
        public async Task<PartialViewResult> Modal(int id)
        {
            var url = string.Format(UriProvider.Content.FeatureAttachmentModal, id);

            var featureAttachmentModalResponseContract = await GetResponseFromService<FeatureAttachmentModalResponseContract>(url);

            var linkToTextTranslatedItem = await GetLinkToTextTranslatedItem();

            var featureAttachmentModalViewModel = featureAttachmentModalResponseContract.ToViewModel();
            featureAttachmentModalViewModel.LinkToTextTranslatedItem = linkToTextTranslatedItem;

            return PartialView("_FeatureAttachmentModal", featureAttachmentModalViewModel);
        }

        public async Task<PartialViewResult> VideoModal(int id)
        {
            var url = string.Format(UriProvider.Content.FeatureAttachmentVideoModal, id);

            var linkToTextTranslatedItem = await GetLinkToTextTranslatedItem();

            var featureAttachmentVideoModalResponseContract = await GetResponseFromService<FeatureAttachmentVideoModalResponseContract>(url);

            var featureAttachmentVideoModalViewModel = featureAttachmentVideoModalResponseContract.ToViewModel();
            featureAttachmentVideoModalViewModel.LinkToTextTranslatedItem = linkToTextTranslatedItem;

            return PartialView("_FeatureAttachmentVideoModal", featureAttachmentVideoModalViewModel);
        }

        private async Task<TranslatedItem> GetLinkToTextTranslatedItem()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest
                {
                    ResourceId = "LinkToFeatureAttachment",
                    ResourceSet = "media/default.aspx"
                }
            });

            return translatedItems.FirstOrDefault();
        }

        // POST: Eylea/FeatureAttachment
        public async Task<JsonResult> FeatureAttachmentsList()
        {
            //var id = pageNumber;
            //var url = string.Format(UriProvider.Content.FeatureAttachmentsList, id);
            var url = UriProvider.Content.FeatureAttachmentsList;

            //var linkToTextTranslatedItem = await GetLinkToTextTranslatedItem();

            var featureAttachmentPageResponseContract = await GetResponseFromService<FeatureAttachmentPageResponseContract>(url);

            var featureAttachmentPageViewModel = featureAttachmentPageResponseContract.ToViewModel();
            //featureAttachmentVideoModalViewModel.LinkToTextTranslatedItem = linkToTextTranslatedItem;

            //return PartialView("_FeatureAttachmentsList", featureAttachmentPageViewModel);

            return Json(featureAttachmentPageViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}