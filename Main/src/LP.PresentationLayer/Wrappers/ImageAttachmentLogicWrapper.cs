using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LP.Api.Shared.Interfaces.Wrappers;
using LP.Model.Dto;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.PresentationLayer.Wrappers
{
    public class ImageAttachmentLogicWrapper : IImageAttachmentLogicWrapper
    {
        public string GetBodyText(string imageTextCssClass, string body)
        {
            var wrapper = string.Empty;
            wrapper = string.Format("<div class='{0}'>{1}</div>", imageTextCssClass, body);
            return wrapper;
        }

        public Image GetHoverImage(string imageOnClick)
        {
            var hoverImage = new Image
            {
                ID = "hover-img",
                AlternateText = @"hover image",
                CssClass = "hover_img",
                ImageUrl = "~/Images/blank.gif"
            };
            hoverImage.Attributes.Add("onclick", imageOnClick);
            return hoverImage;
        }

        public Image GetImage(FeatureAttachmentTranslationDto featureAttachment, string width, string height, string imageOnClick, string systemUrl, string module)
        {
            var image = new Image
            {
                ID = "img" + featureAttachment.FeatureAttachmentID,
                AlternateText = @"Image",
                CssClass = "thumb-img",
                ImageUrl = string.Format(
                  "{0}ThumbNail.ashx?maxWidth={1}&maxHeight={2}&thumb=yes&image={3}&faId={4}&t={5}",
                  systemUrl,
                  width,
                  height,
                  featureAttachment.FileName,
                  featureAttachment.FeatureAttachmentID,
                  featureAttachment.LastUpdated.ToString("yyMMddhhmmss"))
            };
            image.Attributes.Add("onclick", imageOnClick);

            // attributes for hover window data
            image.Attributes.Add("data-title", featureAttachment.Title);
            image.Attributes.Add("data-module", module);
            image.Attributes.Add("data-filetype", "Image File");
            image.Attributes.Add("data-uploaded", featureAttachment.LastUpdated.ToShortDateString());
            return image;
        }

        public string GetOnClick(FeatureAttachmentTranslationDto featureAttachment, string imageUrl, string imageShowerUrl)
        {
           
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return
                    $"javascript:openColorBoxWithScaledImage('{imageShowerUrl}{featureAttachment.FeatureAttachmentID}')";
                //return string.Format(
                //  "javascript:openColorBoxWithScaledImage('{0}{1}');",
                //  imageShowerUrl,
                //  featureAttachment.FeatureAttachmentID
                // );
            }
            return string.Format(
                "javascript:openColorBoxWithScaledImage('{0}{1}'); piwikTracker.trackPageView('{2} clicked (URL: {3}, ID: {1})');",
                imageShowerUrl,
                featureAttachment.FeatureAttachmentID,
                featureAttachment.FeatureAttachment.ltl_FeatureAttachmentType.Type,
                imageUrl);
        }

        public string GetWrapper(string containerClass, string titleClass, string previewClass, int? attachmentStatus)
        {
            var wrapper = string.Empty;
            if (attachmentStatus == (int) Status.TranslationInProgress)
            {
                containerClass += previewClass;
            }
            wrapper = string.Format("<div class='{0}'><div class='imageTop'><span class='{1}'></span></div>", containerClass, titleClass);
            return wrapper;
        }
    }
}