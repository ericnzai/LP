using LP.Model.Mappers;
using LP.Model.ViewModels.TopicTranslation;
using LP.PresentationLayer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using LP.ServiceHost.DataContracts.Request.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class TopicTranslationController : BaseController
    {
        public async Task<PartialViewResult> GetAllTopicCategories()
        {
            var completeTopicTranslationResponseContract =
                await GetResponseFromService<CompleteTopicCategoryTranslationResponseContract>("api/content/topic-category-translation", null);

            var topicCategoryTranslationsViewModel = new CompleteTopicCategoryTranslationViewModel()
            {
                TopicCategoryTranslations = completeTopicTranslationResponseContract.TopicCategoryTranslations.ToViewModels()
            };

            return PartialView("~/Areas/Eylea/Views/TopicTranslation/_TopicCategoryTranslations.cshtml", topicCategoryTranslationsViewModel);
        }

        public async Task<PartialViewResult> GetAllTopicCategoriesByCulture(string culture)
        {
            var url = String.Format("api/content/topic-category-translation/culture/{0}", culture);
            var completeTopicTranslationResponseContract =
                await GetResponseFromService<CompleteTopicCategoryTranslationResponseContract>(url, null);

            var topicCategoryTranslationsViewModel = new CompleteTopicCategoryTranslationViewModel()
            {
                TopicCategoryTranslations = completeTopicTranslationResponseContract.TopicCategoryTranslations.ToViewModels()
            };

            return PartialView("~/Areas/Eylea/Views/TopicTranslation/_TopicCategoryTranslations.cshtml", topicCategoryTranslationsViewModel);
        }

        public async Task<PartialViewResult> GetTopicCategoryTranslation(int id)
        {
            var url = String.Format("api/content/topic-category-translation/{0}", id);
            var topicCategoryTranslationResponseContract =
                await GetResponseFromService<TopicCategoryTranslationFormResponseContract>(url, null);

            var topicCategoryTranslationFormViewModel = new TopicCategoryTranslationFormViewModel()
            {
                GlobalTopicCategoryTranslation = topicCategoryTranslationResponseContract.GlobalTopicCategoryTranslation,
                LocalTopicCategoryTranslation = topicCategoryTranslationResponseContract.LocalCategoryTranslation
            };

            return PartialView("~/Areas/Eylea/Views/TopicTranslation/_AddUpdateTopicCategoryTranslation.cshtml", topicCategoryTranslationFormViewModel);
        }

        public async Task<PartialViewResult> GetTopicTranslation(int id)
        {
            var url = String.Format("api/content/topic-translation/{0}", id);
            var topicTranslationResponseContract =
                await GetResponseFromService<TopicTranslationFormResponseContract>(url, null);

            var topicTranslationFormViewModel = new TopicTranslationFormViewModel()
            {
                GlobalTopicTranslation = topicTranslationResponseContract.GlobalTopicTranslation,
                LocalTopicTranslation = topicTranslationResponseContract.LocalTopicTranslation
            };

            return PartialView("~/Areas/Eylea/Views/TopicTranslation/_AddUpdateTopicTranslation.cshtml", topicTranslationFormViewModel);
        }

        public async Task<JsonResult> AddTopicCategoryTranslation(int topicCategoryId, string topicCategoryName, Status status)
        {
            var requestTopicCategoryTranslation = new TopicCategoryTranslationRequestContract() { TopicCategoryId = topicCategoryId, TopicCategoryName = topicCategoryName, Staus = status };
            var topicCategoryTranslationResponseContract =
                await
                    PostRequestToService<TopicCategoryTranslationRequestContract, TopicCategoryTranslationResponseContract>(
                        "api/content/topic-category-translation/add", requestTopicCategoryTranslation);

            return Json(topicCategoryTranslationResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateTopicCategoryTranslation(int topicCategoryId, string topicCategoryName, Status status)
        {
            var requestTopicCategoryTranslationUpdate = new TopicCategoryTranslationUpdateRequestContract()
            {
                TopicCategoryId = topicCategoryId,
                TopicCategoryName = topicCategoryName,
                Staus = status
            };
            var topicCategoryTranslationUpdateResponseContract =
                await
                    PutRequestToService<TopicCategoryTranslationUpdateRequestContract, TopicCategoryTranslationUpdateResponseContract>(
                        "api/content/topic-category-translation/update", requestTopicCategoryTranslationUpdate);

            return Json(topicCategoryTranslationUpdateResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddTopicTranslation(int topicId, string topicName, Status status)
        {
            var requestTopicTranslation = new TopicTranslationRequestContract() { TopicId = topicId, TopicName = topicName, Staus = status };
            var topicTranslationResponseContract =
                await
                    PostRequestToService<TopicTranslationRequestContract, TopicTranslationResponseContract>("api/content/topic-translation/add",
                        requestTopicTranslation);

            return Json(topicTranslationResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateTopicTranslation(int topicId, string topicName, Status status)
        {
            var requestTopicTranslationUpdate = new TopicTranslationUpdateRequestContract()
            {
                TopicId = topicId,
                TopicName = topicName,
                Staus = status
            };
            var topicTranslationUpdateResponseContract =
                await
                    PutRequestToService<TopicTranslationUpdateRequestContract, TopicTranslationUpdateResponseContract>(
                        "api/content/topic-translation/update", requestTopicTranslationUpdate);

            return Json(topicTranslationUpdateResponseContract.Result, JsonRequestBehavior.AllowGet);
        }
    }
}