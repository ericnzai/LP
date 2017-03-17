using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.BusinessLayer.Content.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Providers;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.Dto;
using LP.Model.Mappers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Common.Content.FeatureAttachment;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;

namespace LP.Content.BusinessLayer.Commands
{
    public class FeatureAttachmentCommands : IFeatureAttachmentCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IUrlMapperCommands _urlMapperCommands;
        private readonly IFeatureAttachmentFilter _featureAttachmentFilter;
        private readonly ICommonCalculatorCommands _commonCalculatorCommands;
        private readonly IPostCommands _postCommands;
        private readonly IGroupCommands _groupCommands;

        private const int NumberOfFeatureAttachmentsPerPage = 20;

        public FeatureAttachmentCommands(IBaseCommands baseCommands, IUrlMapperCommands urlMapperCommands, IFeatureAttachmentFilter featureAttachmentFilter, ICommonCalculatorCommands commonCalculatorCommands, IPostCommands postCommands, IGroupCommands groupCommands)
        {
            _baseCommands = baseCommands;
            _urlMapperCommands = urlMapperCommands;
            _featureAttachmentFilter = featureAttachmentFilter;
            _commonCalculatorCommands = commonCalculatorCommands;
            _postCommands = postCommands;
            _groupCommands = groupCommands;
        }

        public async Task<FeatureAttachmentModalResponseContract> GetFeatureAttachmentModalResponseContract(
            int featureAttachmentId, UserDetails userDetails)
        {
            var featureAttachment = await GetFeatureAttachmentWithTranslationsAsync(featureAttachmentId);

            var featureAttachmentPostInformation = await GetFeatureAttachmentPostInformation(featureAttachment, userDetails.CurrentCulture);
            
            var imageUrl = await GetImageUrlFromFeatureAttachment(featureAttachment, userDetails.CurrentCulture);

            var featureAttachmentTranslation = GetTranslation(featureAttachment, userDetails.CurrentCulture);

            return new FeatureAttachmentModalResponseContract
            {
                FeatureAttachmentId = featureAttachment.FeatureAttachmentID,
                Title = featureAttachmentTranslation.Title,
                Description = featureAttachmentTranslation.Body,
                PopupText = featureAttachmentTranslation.PopupText,
                ImageUrl = imageUrl,
                FeatureAttachmentPostInformation = featureAttachmentPostInformation
            };
        }

        public async Task<FeatureAttachmentVideoModalResponseContract> GetFeatureAttachmentVideoModalResponseContract(
            int featureAttachmentId, UserDetails userDetails)
        {
            var featureAttachment = await GetFeatureAttachmentWithTranslationsAsync(featureAttachmentId);

            var featureAttachmentPostInformation = await GetFeatureAttachmentPostInformation(featureAttachment, userDetails.CurrentCulture);

            var featureAttachmentTranslation = GetTranslation(featureAttachment, userDetails.CurrentCulture);

            return new FeatureAttachmentVideoModalResponseContract
            {
                FeatureAttachmentId = featureAttachment.FeatureAttachmentID,
                Title = featureAttachmentTranslation.Title,
                Description = featureAttachmentTranslation.Body,
                PopupText = featureAttachmentTranslation.PopupText,
                FeatureAttachmentPostInformation = featureAttachmentPostInformation
            };
        }

        public async Task<FeatureAttachmentPageResponseContract> GetFeatureAttachmentPageAsync(int pageNumber, UserDetails userDetails)
        {
            var featureAttachments = await _featureAttachmentFilter.FilterAllowedFeatureAttachmentTranslations(userDetails);

            var numberToSkip = _commonCalculatorCommands.GetPagingNumberToSkip(pageNumber, NumberOfFeatureAttachmentsPerPage);

            var featureAttachmentsForPage = featureAttachments.OrderBy(so => so.FeatureAttachmentID).Skip(numberToSkip).Take(NumberOfFeatureAttachmentsPerPage);

            var featureAttachmentPageResponseContract = new FeatureAttachmentPageResponseContract
            {
                FeatureAttachmentItems = featureAttachmentsForPage.ToItemContracts(),
                PageNumber = pageNumber
            };

            return featureAttachmentPageResponseContract;
        }

        
        public async Task<string> GetAttachmentVirtualUrl(FeatureAttachmentTranslationDto fatDto)
        {
            var groupByFeatureAttachmentIdGroups = await _groupCommands.GetGroupByFeatureAttachmentIdGroups(fatDto.FeatureAttachment.FeatureAttachmentID);

            var groupFriendlyUrl = groupByFeatureAttachmentIdGroups.FriendlyUrl;
            
            var type = GetTypeFolder(fatDto.FeatureAttachment.Type());

            //var filename = (string.IsNullOrWhiteSpace(fatDto.Culture))
            //    ? fatDto.FeatureAttachment.FeatureAttachmentTranslations.First().FileName
            //    : fatDto.FeatureAttachment.FeatureAttachmentTranslations.Single(fat => fat.Culture == fatDto.Culture).FileName;

            var filename = fatDto.FileName;

            var cacheTimeStamp = fatDto.LastUpdated.ToString("yyMMddhhmmss");

            if (type == "WebCasts")
            {
                //var webCastValue = _featureAttachmentsBusiness.GetFeatureAttachmentCustomField(fatDto.FeatureAttachment.FeatureAttachmentID, "SpeakerImage").Value;

                var speaker =
                    fatDto.FeatureAttachment.ltl_FeatureAttachment_CustomField.FirstOrDefault(
                        c => c.ltl_FeatureAttachment_CustomFieldDefinition.CustomFieldName == "SpeakerImage");

                if (speaker != null)
                {
                    var webCastValue = speaker.Value;
                    filename = string.Format("{0}/Images/{1}", fatDto.FeatureAttachment.FeatureAttachmentID, webCastValue);
                }
            }

            return string.Format("~/Content/{0}/FeatureAttachments/{1}/{2}?t={3}", groupFriendlyUrl, type, filename, cacheTimeStamp);
        }

        private static string GetTypeFolder(AttachmentType type)
        {
            switch (type)
            {
                case AttachmentType.Video:
                    return "Videos";
                case AttachmentType.Image:
                    return "Images";
                case AttachmentType.WebCast:
                    return "WebCasts";
                case AttachmentType.Other:
                    return String.Empty;
            }

            throw new ArgumentException();
        }

        private async Task<FeatureAttachmentPostInformation> GetFeatureAttachmentPostInformation(ltl_FeatureAttachment featureAttachment, string currentCulture)
        {
            if (!featureAttachment.CSPostID.HasValue) return new FeatureAttachmentPostInformation();

            var post = await GetPostWithIncludesAsync(featureAttachment.CSPostID.Value);

            var parentSection = await _baseCommands.GetByIdWithIncludesAsync<ltl_Sections>(x => x.SectionID == post.ltl_Sections.ParentID, x=>x.ltl_SectionTranslations);
            var parentTitle = string.Empty;
            if (parentSection != null)
            {
                var parent = parentSection.ltl_SectionTranslations.FirstOrDefault(c => c.Culture == currentCulture);
                parentTitle = parent != null ? parent.Name : string.Empty;
            }
            var sectionTitle = GetSectionName(post, currentCulture);
            var postTitle = GetPostName(post, currentCulture);
            return new FeatureAttachmentPostInformation
            {
                ParentSectionTitle = parentTitle,
                GroupName = post.ltl_Sections.ltl_Groups.Name,
                PostUrl = _urlMapperCommands.MapUrlForPost(post),
                PostTitle = postTitle,
                SectionTitle = sectionTitle
            };
        }

        private ltl_FeatureAttachmentTranslation GetTranslation(ltl_FeatureAttachment featureAttachment, string currentCulture)
        {
            var translations = featureAttachment.ltl_FeatureAttachmentTranslation;

            var currentCultureTranslation = translations.FirstOrDefault(a => a.Culture == currentCulture);

            if (currentCultureTranslation != null)
            {
                return currentCultureTranslation;
            }

            var globalCultureTranslation = translations.FirstOrDefault(a => a.Culture == ConstantProvider.GlobalCulture);

            if (globalCultureTranslation != null) return globalCultureTranslation;

            return null;
        }

        private async Task<ltl_FeatureAttachment> GetFeatureAttachmentWithTranslationsAsync(int featureAttachmentId)
        {
            var featureAttachments = await _baseCommands.GetWithIncludesAsync<ltl_FeatureAttachment>(inc => inc.ltl_FeatureAttachmentTranslation);

            return await featureAttachments.FirstOrDefaultAsync(a => a.FeatureAttachmentID == featureAttachmentId);
        }

        private static string GetSectionName(ltl_Posts post, string currentCulture)
        {
            var sectionTranslations = post.ltl_Sections.ltl_SectionTranslations;
            
            var sectionTranslation = sectionTranslations.FirstOrDefault(a => a.Culture == currentCulture);

            if (sectionTranslation != null)
            {
                return sectionTranslation.Name;
            }

            var globalSectionTranslation = sectionTranslations.FirstOrDefault(a => a.Culture == ConstantProvider.GlobalCulture);

            return globalSectionTranslation != null ? globalSectionTranslation.Name : string.Empty;
        }

        private static string GetPostName(ltl_Posts post, string currentCulture)
        {
            var postTranslations = post.ltl_PostTranslations;

            var postTranslation = postTranslations.FirstOrDefault(a => a.Culture == currentCulture);

            if (postTranslation != null)
            {
                return !string.IsNullOrEmpty(postTranslation.PostName) ? postTranslation.PostName : postTranslation.Subject;
            }

            var globalPostTranslation = postTranslations.FirstOrDefault(a => a.Culture == ConstantProvider.GlobalCulture);

            return globalPostTranslation != null ? globalPostTranslation.PostName : string.Empty;
        }

        private async Task<string> GetImageUrlFromFeatureAttachment(ltl_FeatureAttachment featureAttachment, string currentCulture)
        {
            var imageUrl = string.Empty;

            if (featureAttachment.CSPostID.HasValue)
            {
                imageUrl = await _urlMapperCommands.MapUrlForFeatureAttachmentImage(featureAttachment, currentCulture);
            }

            return imageUrl;
        }

        private async Task<ltl_Posts> GetPostWithIncludesAsync(int postId)
        {
            var posts = await _baseCommands.GetWithIncludesAsync<ltl_Posts>(inc => inc.ltl_Sections.ltl_Groups.TrainingArea, a => a.ltl_PostTranslations, a => a.ltl_Sections.ltl_SectionTranslations);

            var post = await posts.FirstOrDefaultAsync(p => p.PostID == postId);

            return post;
        }
    }
}
