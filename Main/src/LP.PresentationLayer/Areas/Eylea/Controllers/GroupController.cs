using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Model.ViewModels.Group;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Eylea/Group
        public async Task<PartialViewResult> Groups()
        {
            var groupResponseContract =
                await GetResponseFromService<GroupResponseContract>("api/content/group", null);

            var groupsViewModel = new GroupsViewModel()
            {
                Groups = groupResponseContract.Groups
            };

            return PartialView("~/Areas/Eylea/Views/Group/_GroupLists.cshtml", groupsViewModel);
        }
        
        // GET: Eylea/Group
        public async Task<PartialViewResult> GroupsWithIds()
        {
            var groupResponseContract =
                await GetResponseFromService<GroupResponseContract>("api/content/group", null);

            var groupsViewModel = new GroupsViewModel()
            {
                Groups = groupResponseContract.Groups
            };

            return PartialView("~/Areas/Eylea/Views/Group/_GroupListsWithIds.cshtml", groupsViewModel);
        }
        
        // GET: Eylea/Group
        public async Task<PartialViewResult> GroupsDropdown()
        {
            var translatedItems = await RequestTranslatedItems(new List<TranslationRequest>
                {
                    new TranslationRequest
                    {
                        ResourceId = "optAll",
                        ResourceSet = "Master/Master.aspx"
                    }
                });
            
            var groupResponseContract =
                await GetResponseFromService<GroupResponseContract>("api/content/group", null);
            var translatedAll = translatedItems.FirstOrDefault(x => x.ResourceId == "optAll");

            var listItems = new List<Group> { new Group() { Id = 0, Name = translatedAll != null ? translatedAll.TranslatedValue : string.Empty } };
            listItems.AddRange(groupResponseContract.Groups);

            var groupsViewModel = new GroupsViewModel()
            {
                Groups = listItems
            };

            return PartialView("~/Areas/Eylea/Views/Group/_GroupDropdown.cshtml", groupsViewModel);
        }
    }
}