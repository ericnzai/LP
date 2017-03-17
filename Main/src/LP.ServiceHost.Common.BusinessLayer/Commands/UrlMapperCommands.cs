using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Providers;
using LP.EntityModels;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class UrlMapperCommands : IUrlMapperCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IConfigurationProvider _configurationProvider;
        public UrlMapperCommands(IBaseCommands baseCommands, IConfigurationProvider configurationProvider)
        {
            _baseCommands = baseCommands;
            _configurationProvider = configurationProvider;
        }

        public async Task<string> MapUrlForPost(int postId)
        {
            var post = await GetPostWithIncludesAsync(postId);

            return post == null ? string.Empty : MapUrlForPost(post);
        }

        public string MapUrlForPost(ltl_Posts post)
        {
            if (post == null) return string.Empty;

            var frontEndWebUrl = _configurationProvider.FrontEndWebUrl;

            return string.Format("{0}{1}", frontEndWebUrl, post.ltl_Sections.ltl_Groups.TrainingArea.FriendlyUrl + "/" + post.ltl_Sections.ltl_Groups.FriendlyUrl + "/" +
                    post.ltl_Sections.FriendlyUrl + "/Page" + post.SortOrder);
        }

        public async Task<string> MapUrlForFeatureAttachmentImage(ltl_FeatureAttachment featureAttachment, string currentCulture)
        {
            if (featureAttachment == null || featureAttachment.CSPostID == null) return string.Empty;

            var post = await GetPostWithIncludesAsync(featureAttachment.CSPostID.Value);
   
            if (post == null || post.ltl_Sections == null || post.ltl_Sections.ltl_Groups == null) return string.Empty;

            var groupFriendlyUrl = post.ltl_Sections.ltl_Groups.FriendlyUrl;

            var frontEndWebUrl = _configurationProvider.FrontEndWebUrl;

            var featureAttachmentTranslations =
              await  _baseCommands.GetConditionalAsync<ltl_FeatureAttachmentTranslation>(cond => cond.FeatureAttachmentID == featureAttachment.FeatureAttachmentID);

            var selectedFeatureAttachmentTranslation = featureAttachmentTranslations.FirstOrDefault(a => a.Culture == currentCulture) ??
                                                       featureAttachmentTranslations.FirstOrDefault(a => a.Culture == ConstantProvider.GlobalCulture);

            return selectedFeatureAttachmentTranslation == null ? string.Empty : string.Format("{0}Content/{1}/FeatureAttachments/Images/{2}", frontEndWebUrl, groupFriendlyUrl, selectedFeatureAttachmentTranslation.FileName);
        }

        private async Task<ltl_Posts> GetPostWithIncludesAsync(int postId)
        {
            var posts = await _baseCommands.GetWithIncludesAsync<ltl_Posts>(inc => inc.ltl_Sections.ltl_Groups.TrainingArea);

            return await posts.FirstOrDefaultAsync(p => p.PostID == postId);

        }
    }
}
