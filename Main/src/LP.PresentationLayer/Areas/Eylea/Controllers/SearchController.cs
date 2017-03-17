using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Model.ViewModels.Search;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class SearchController : BaseController
    {
        // POST: Eylea/Search
        public async Task<JsonResult> SearchItems( string searchTerm, string groupTypeId, string topicIds)
        {
            var requestSearch = new SearchRequestContract() { SearchTerm = searchTerm, GroupTypeId = groupTypeId, TopicIds = topicIds};
            var searchItemResponseContract =
                await PostRequestToService<SearchRequestContract, SearchItemsResponseContract>("api/content/search-item", requestSearch);

            var searchItemsViewModel = new SearchItemsViewModel()
            {
                SearchItems = searchItemResponseContract.SearchItems
            };

            return Json(searchItemsViewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Eylea/Search
        public async Task<PartialViewResult> SearchTitle()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
            {
                new TranslationRequest
                {
                    ResourceId = "ltSearchResults.Text",
                    ResourceSet = "Master/Master.aspx"
                }
            });

            var translatedItem = translatedItems.FirstOrDefault();

            return PartialView("~/Views/Shared/Partial/_SingleTranslatedItem.cshtml", new TranslatedItem() { TranslatedValue = translatedItem.TranslatedValue, ResourceId = translatedItem.ResourceId, ResourceSet = translatedItem.ResourceSet });
        }
    }
}