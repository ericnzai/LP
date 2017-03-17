using System;
using LP.EntityModels;

namespace LP.Model.Dto
{
    public class FeatureAttachmentTranslationDto
    {
        public int FeatureAttachmentID { get; set; }

        public string Culture { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int? Status { get; set; }

        public string Extra { get; set; }

        public string Parameters { get; set; }

        public string PopupText { get; set; }

        public DateTime LastUpdated { get; set; }

        public int LastUpdatedByUserID { get; set; }

        public int UserID { get; set; }

        public FeatureAttachmentDto FeatureAttachment { get; set; }

        public string Template { get; set; }

        public int SortOrder { get; set; }
    }
}
