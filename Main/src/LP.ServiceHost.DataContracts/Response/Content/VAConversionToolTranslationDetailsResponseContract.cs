using System;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class VAConversionToolTranslationDetailsResponseContract
    {
        public string Culture { get; set; }
        public string CultureDisplayName { get; set; }
        public string FileName { get; set; }
        public string DownloadFilePath { get; set; }
        public string Content { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedDateString { get; set; }
        public string LastUpdatedTimeString { get; set; }
        public List<VAConversionToolContract> VAConversionToolTranslations { get; set; }
        public string History { get; set; }
        public bool IsTranslationCompleted { get; set; }
    }
}
