using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LP.Model.ViewModels.Shared;
using LP.Model.ViewModels.Topic;
using LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class TopicController : BaseController
    {
        // POST: Eylea/Topic
        public async Task<JsonResult> AddTopicCategory(string categoryName)
        {
            var requestTopicCategory = new TopicCategoryRequestContract() {CategoryName = categoryName};
            var topicCategoryResponseContract =
                await
                    PostRequestToService<TopicCategoryRequestContract, TopicCategoryResponseContract>(
                        "api/content/topic-category/add", requestTopicCategory);

            return Json(topicCategoryResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // GET: Eylea/Topic
        public async Task<JsonResult> GetAllTopicCategories()
        {
            var topicCategoryResponseContract =
                await GetResponseFromService<TopicCategoriesResponseContract>("api/content/topic-category", null);

            var topicCategoriesViewModel = new TopicCategoriesViewModel()
            {
                TopicCategories = topicCategoryResponseContract.TopicCategories
            };

            return Json(topicCategoriesViewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Eylea/Topic
        public async Task<PartialViewResult> AddTopicView()
        {
            var topicCategoryResponseContract =
                await GetResponseFromService<TopicCategoriesResponseContract>("api/content/topic-category", null);

            var topicViewModel = new TopicViewModel()
            {
                TopicCategories = new DropdownViewModel()
                {
                    DropdownItems =
                        topicCategoryResponseContract.TopicCategories.Select(t => new DropdownItemViewModel()
                        {
                            Id = t.CategoryId.ToString(),
                            Name = t.CategoryName
                        }).ToList(),
                    ClientId = "ddlTopicCategories"
                },

            };

            return PartialView("~/Areas/Eylea/Views/Topic/_AddTopic.cshtml", topicViewModel);
        }

        // POST: Eylea/Topic
        public async Task<PartialViewResult> AddTopicsDropdownWithCheckboxesView(int postId)
        {
            var requestPostTopic = new PostTopicRequestContract() {PostId = postId};

            var postTopicResponseContract =
                await
                    PostRequestToService<PostTopicRequestContract, PostTopicResponseContract>("api/content/post-topic",
                        requestPostTopic);

            var topicsDropdownViewModel = new TopicDropdownWithCheckboxesViewModel()
            {
                Items =
                    postTopicResponseContract.CategoryTopics.Select(t => new DropdownItemViewModel()
                    {
                        //Id = string.Format("{0}_{1}", t.CategoryId, t.TopicId),
                        Id = t.Id,
                        Name = t.Name,
                        Checked = t.Checked,
                        DisplayCheckbox = !t.IsCategory
                    }).ToList()
            };

            return PartialView("~/Areas/Eylea/Views/Topic/Partial/_TopicsDropdownWithCheckboxes.cshtml",
                topicsDropdownViewModel);
        }

        // GET: Eylea/Topic
        public async Task<JsonResult> GetAllTopics()
        {
            var topicResponseContract =
                await GetResponseFromService<TopicsResponseContract>("api/content/topic", null);

            var topicsViewModel = new TopicsViewModel()
            {
                Topics = topicResponseContract.Topics.Select(t => new TopicViewModel()
                {
                    TopicName = t.TopicName,
                    TopicCategory = t.TopicCategoryName,
                    TopicId = t.TopicId,
                    CategoryId = t.CategoryId,
                    Category =
                        new TopicCategoryViewModel() {CategoryId = t.CategoryId, CategoryName = t.TopicCategoryName},
                }).ToList(),
                Categories = topicResponseContract.TopicCategories.ToList()
            };

            return Json(topicsViewModel, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> AddTopic(string name, int categoryId)
        {
            var requestTopic = new TopicRequestContract() {TopicName = name, CategoryId = categoryId};
            var topicResponseContract =
                await
                    PostRequestToService<TopicRequestContract, TopicResponseContract>("api/content/topic/add",
                        requestTopic);

            return Json(topicResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> DeleteTopicCategory(int categoryId)
        {
            var url = string.Format("api/content/topic-category/delete/{0}", categoryId);

            var topicCategoryDeleteResponseContract =
                await DeleteRequestToService<TopicCategoryDeleteResponseContract>(url);

            return Json(topicCategoryDeleteResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> UpdateTopicCategory(int categoryId, string categoryName)
        {
            var requestTopicCategoryUpdate = new TopicCategoryUpdateRequestContract()
            {
                CategoryId = categoryId,
                CategoryName = categoryName
            };
            var topicCategoryUpdateResponseContract =
                await
                    PutRequestToService<TopicCategoryUpdateRequestContract, TopicCategoryUpdateResponseContract>(
                        "api/content/topic-category/update", requestTopicCategoryUpdate);

            return Json(topicCategoryUpdateResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> DeleteTopic(int topicId)
        {
            var url = string.Format("api/content/topic/delete/{0}", topicId);

            var topicDeleteResponseContract =
                await DeleteRequestToService<TopicDeleteResponseContract>(url);

            return Json(topicDeleteResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> UpdateTopic(int topicId, int categoryId, string topicName)
        {
            var requestTopicUpdate = new TopicUpdateRequestContract()
            {
                TopicId = topicId,
                CategoryId = categoryId,
                TopicName = topicName
            };
            var topicUpdateResponseContract =
                await
                    PutRequestToService<TopicUpdateRequestContract, TopicUpdateResponseContract>(
                        "api/content/topic/update", requestTopicUpdate);

            return Json(topicUpdateResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // POST: Eylea/Topic
        public async Task<JsonResult> SavePostTopics(string topics, string postId)
        {
            var requestPostTopic = new PostTopicsRequestContract() {Topics = topics, PostId = postId};
            var topicResponseContract =
                await
                    PostRequestToService<PostTopicsRequestContract, TopicResponseContract>(
                        "api/content/post-topic/add", requestPostTopic);

            return Json(topicResponseContract.Result, JsonRequestBehavior.AllowGet);
        }

        // GET: Eylea/Topic
        public async Task<PartialViewResult> TopicsWithIds()
        {
            var postTopicResponseContract =
                await GetResponseFromService<PostTopicResponseContract>("api/content/post-topic/search", null);

            var categoryTopicsViewModel = new CategoryTopicsViewModel()
            {
                Items = postTopicResponseContract.CategoryTopics
            };

            return PartialView("~/Areas/Eylea/Views/Topic/Partial/_TopicListsWithIds.cshtml", categoryTopicsViewModel);
        }

        // GET: Eylea/Topic
        public async Task<PartialViewResult> PostTopics(int postId)
        {
            var requestPostTopic = new PostTopicRequestContract() {PostId = postId};

            var postTopicResponseContract =
                await
                    PostRequestToService<PostTopicRequestContract, PostTopicResponseContract>("api/content/post-topic",
                        requestPostTopic);

            var topicList =
                postTopicResponseContract.CategoryTopics.Where(c => c.IsCategory == false && c.Checked == true).Select(c => c.Name).ToList();

            return PartialView("~/Areas/Eylea/Views/Topic/Partial/_PostTopicsList.cshtml", topicList);
        }
    }
}