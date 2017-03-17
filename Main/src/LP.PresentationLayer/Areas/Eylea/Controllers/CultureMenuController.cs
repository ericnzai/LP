using LP.Model.ViewModels.Common;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Response.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class CultureMenuController : BaseController
    {
        public async Task<JsonResult> GetAvailableCulturesMenu()
        {
            var completeCultureMenuResponseContract =
                await GetResponseFromService<CompleteCultureMenuResponseContract>("api/content/culture-menu", null);

            var cultureMenuViewModel = new CultureMenuViewModel()
            {
                AvailableCultures = completeCultureMenuResponseContract.AvailableCultures
            };

            return Json(cultureMenuViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}