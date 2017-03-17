using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using LP.Model.Dto;

namespace LP.Api.Shared.Interfaces.Wrappers
{
    public interface IImageAttachmentLogicWrapper
    {
        string GetWrapper(string containerClass, string titleClass, string previewClass, int? attachmentStatus);
        Image GetHoverImage(string imageOnClick);
        string GetOnClick(FeatureAttachmentTranslationDto featureAttachment, string imageUrl, string imageShowerUr);

        Image GetImage(FeatureAttachmentTranslationDto featureAttachment, string width, string height,
            string imageOnClick, string systemUrl, string module);

        string GetBodyText(string imageTextCssClass, string body);
    }
}
