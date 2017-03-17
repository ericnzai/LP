using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Model.ViewModels.Shared;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Response.Authentication;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class UserController : BaseController
    {
        public async Task<PartialViewResult> Information()
        {
            var userInformationResponseContract =
                await
                    GetResponseFromService<UserInformationResponseContract>("api/authentication/user-information", null);

            var userInformationViewModel = new UserInformationViewModel
            {
                FieldOfEmploymenTranslatedItem = new TranslatedItem { TranslatedValue = "Field of Employment"},
                FieldOfEmployment = userInformationResponseContract.FieldOfEmployment,
                UserCountry = userInformationResponseContract.UserCountry,
                UserDisplayName = userInformationResponseContract.DisplayName
            };

            return PartialView("~/Areas/Eylea/Views/Dashboard/Partial/_UserInformation.cshtml", userInformationViewModel);
        }
    }
}