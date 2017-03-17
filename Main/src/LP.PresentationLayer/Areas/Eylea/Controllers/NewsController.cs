using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Model.ViewModels.News;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class NewsController : BaseController
    {
        public async Task<PartialViewResult> NewsAndAnnouncements()
        {
            //var readMoreTranslatedItem = new TranslatedItem {TranslatedValue = "Read More"};
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
                {
                    new TranslationRequest
                    {
                        ResourceId = "lnkReadMoreResource1.Text",
                        ResourceSet = "uccontrols/news/UCNews.ascx"
                    },
                    new TranslationRequest
                    {
                        ResourceSet = "uccontrols/news/UCNews.ascx",
                        ResourceId = "ltNewsAndAnnouncementsResource1.Text"
                    }
                });

            var latestNewsResponseContract =
                await GetResponseFromService<LatestNewsResponseContract>("api/content/latest-news", null);

            
            var newsViewModel = new NewsViewModel
            {
                LatestNewsItems = latestNewsResponseContract.LatestNewsItems,
                NewsAndAnnouncementsTranslatedItem = translatedItems.FirstOrDefault(x => x.ResourceId == "ltNewsAndAnnouncementsResource1.Text"),
                ReadMoreTranslatedItem = translatedItems.FirstOrDefault(x => x.ResourceId == "lnkReadMoreResource1.Text")
            };

            return PartialView("~/Areas/Eylea/Views/News/_NewsAndAnnouncements.cshtml", newsViewModel);
        }

    }
}