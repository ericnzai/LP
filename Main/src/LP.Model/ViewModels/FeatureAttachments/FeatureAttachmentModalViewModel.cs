﻿using LP.ServiceHost.DataContracts.Common.Translation;

namespace LP.Model.ViewModels.FeatureAttachments
{
    public class FeatureAttachmentModalViewModel
    {
        public int FeatureAttachmentId { get; set; }
        public FeatureAttachmentPostInformationViewModel FeatureAttachmentPostInformationViewModel { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PopupText { get; set; }
        public TranslatedItem LinkToTextTranslatedItem { get; set; }
    }
}
