using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.BusinessLayer.Content.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.Dto;

namespace LP.Content.BusinessLayer.Filters
{
    public class FeatureAttachmentFilter : IFeatureAttachmentFilter
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IPostPermissionFilter _postPermissionFilter;
        public FeatureAttachmentFilter(IBaseCommands baseCommands, IPostPermissionFilter postPermissionFilter)
        {
            _baseCommands = baseCommands;
            _postPermissionFilter = postPermissionFilter;
        }

        public async Task<IQueryable<ltl_FeatureAttachment>> FilterAllowedFeatureAttachments(UserDetails userDetails)
        {
            var currentAllowedPostIds = await _postPermissionFilter.AllowedLivePostIds(userDetails);

            var featureAttachments =
                await
                    _baseCommands.GetConditionalWithIncludesAsync<ltl_FeatureAttachment>(ft =>
                        userDetails.AvailableStatuses.Contains((int)ft.Status) &&
                        (ft.ltl_FeatureAttachmentType.Type.ToLower().Contains("video")
                        || ft.ltl_FeatureAttachmentType.Type.ToLower().Contains("image")
                        && currentAllowedPostIds.Contains(ft.ltl_Posts.PostID)),
                        fat => fat.ltl_FeatureAttachmentType,
                        fac => fac.ltl_FeatureAttachmentCategory,
                        fat => fat.ltl_FeatureAttachmentTranslation,
                        post => post.ltl_Posts,
                        section => section.ltl_Posts.ltl_Sections);

            return featureAttachments;
        }

        public async Task<IEnumerable<FeatureAttachmentTranslationDto>> FilterAllowedFeatureAttachmentTranslations(UserDetails userDetails)
        {
            var featureAttachments = await FilterAllowedFeatureAttachments(userDetails);

            var featureAttachmentsList = await featureAttachments.ToListAsync();

            return featureAttachmentsList.SelectMany(f => f.ltl_FeatureAttachmentTranslation
                .Where(f1 => f1.Culture == userDetails.CurrentCulture), (f1, f2) => 
                    new FeatureAttachmentTranslationDto
                    {
                        FeatureAttachment =  new FeatureAttachmentDto(f1), 
                        Body = f2.Body,
                        Title = f2.Title,
                        Culture = f2.Culture,
                        PopupText = f2.PopupText,
                        Extra = f2.Extra,
                        FeatureAttachmentID = f2.FeatureAttachmentID,
                        FileName = f2.FileName,
                        LastUpdated = f2.LastUpdated,
                        LastUpdatedByUserID = f2.LastUpdatedByUserID,
                        Parameters = f2.Parameters,
                        Status = f2.Status,
                        UserID = f2.UserID
                    });
        }
    }
}
