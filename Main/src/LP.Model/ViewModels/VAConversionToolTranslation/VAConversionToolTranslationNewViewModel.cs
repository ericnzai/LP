using System;

namespace LP.Model.ViewModels.VAConversionToolTranslation
{
    public class VAConversionToolTranslationNewViewModel
    {
        public string Culture { get; set; }

        public string CultureDisplayName { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }

        public string SaveUrl { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsTranslationCompleted { get; set; }
    }
}
