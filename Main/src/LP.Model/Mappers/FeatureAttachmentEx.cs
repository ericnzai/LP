using System.Collections.Generic;
using LP.EntityModels;
using LP.Model.Dto;
using LP.Model.ViewModels.FeatureAttachments;
using LP.ServiceHost.DataContracts.Common.Content.FeatureAttachment;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentEx
    {
        public static List<FeatureAttachmentItemContract> ToItemContracts(this IEnumerable<FeatureAttachmentTranslationDto> featureAttachments)
        {
            var featureAttachmentItemContracts = new List<FeatureAttachmentItemContract>();

            if (featureAttachments == null) return featureAttachmentItemContracts;

            foreach (var featureAttachment in featureAttachments)
            {
                var featureAttachmentItemContract = new FeatureAttachmentItemContract
                {
                    FeatureAttachmentId = featureAttachment.FeatureAttachmentID,
                    FeatureAttachmentTypeId = featureAttachment.FeatureAttachment.FeatureAttachmentTypeID,
                    FileName = featureAttachment.FileName,
                    Title = featureAttachment.Title,
                    Body = featureAttachment.Body,
                    Parameters = featureAttachment.Parameters
                };

                switch (featureAttachment.FeatureAttachment.FeatureAttachmentTypeID)
                {
                    case 1:
                        //CreateFlashObject(featureAttachment);
                        break;
                    case 2:
                        //CreatePopupVideoObject(featureAttachment);
                        break;
                    case 3:
                        //CreateImageObject(featureAttachment, fat);
                        break;
                    case 4:
                        //CreateTextObject(featureAttachment);
                        break;
                    case 5:
                        //CreatePopupTextObject(featureAttachment);
                        break;
                    case 6:
                        //CreatePopupLinkObject(featureAttachment);
                        break;
                    case 7:
                        //CreatePopupVideoObject(featureAttachment);
                        break;
                    case 8:
                        //CreateRedirectTimer(featureAttachment);
                        break;
                    case 9:
                        //CreateDisclaimerPopup(featureAttachment);
                        break;
                    case 11:
                        //CreateImageLinkObject(featureAttachment);
                        break;
                    case 12:
                    //iPad Video
                    case 14:
                    //Captions Video
                    case 16:
                        //CreateUniversalSubtitlesVideoObject(featureAttachment, false);
                        break;
                    case 13:
                    //iPad Video in popup
                    case 15:
                    //Captions Video in popup
                    case 17:
                        //CreateUniversalSubtitlesVideoObject(featureAttachment, true);
                        break;
                    case 18:
                        //CreateWebcastObject(featureAttachment);
                        break;
                    case 19:
                        //CreateReferenceObject(featureAttachment);
                        break;

                }

                if (featureAttachment.FeatureAttachment.SortOrder.HasValue)
                    featureAttachmentItemContract.SortOrder = featureAttachment.FeatureAttachment.SortOrder.Value;
                featureAttachmentItemContracts.Add(featureAttachmentItemContract);
            }

            return featureAttachmentItemContracts;
        }

        public static FeatureAttachmentViewModel ToViewModel(
            this FeatureAttachmentItemContract featureAttachmentItemResponseContract)
        {
            return new FeatureAttachmentViewModel
            {
                FeatureAttachmentId = featureAttachmentItemResponseContract.FeatureAttachmentId
                //Description = featureAttachmentModalResponseContract.Description,
                //ImageUrl = featureAttachmentModalResponseContract.ImageUrl,
                //Title = featureAttachmentModalResponseContract.Title,
                //FeatureAttachmentPostInformationViewModel = featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.ToViewModel(),
                //FeatureAttachmentId = featureAttachmentModalResponseContract.FeatureAttachmentId
            };
        }
    }
}
