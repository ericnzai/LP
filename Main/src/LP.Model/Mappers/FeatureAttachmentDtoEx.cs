using LP.Model.Dto;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.Mappers
{
    public static class FeatureAttachmentDtoEx
    {
        public static AttachmentType Type(this FeatureAttachmentDto attachment)
        {
            switch (attachment.FeatureAttachmentTypeID)
            {
                case 2:
                case 7:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    return AttachmentType.Video;
                case 3:
                case 11:
                    return AttachmentType.Image;
                case 18:
                    return AttachmentType.WebCast;
                default:
                    return AttachmentType.Other;
            }
        }
    }
}
