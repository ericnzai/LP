using System;
using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class VAConversionToolDetailsResponseContract
    {
        public string Culture { get; set; }
        public string CultureDisplayName { get; set; }
        public string FileName { get; set; }
        public string DownloadFilePath { get; set; }
        public string Content { get; set; }
        public string Comments { get; set; }
        public DateTime LastUpdated { get; set; }
        public string CreatedByUser { get; set; }
        public List<VAConversionToolContract> VAConversionTools { get; set; }
        public List<LanguageContract> Languages { get; set; }
        public bool IsTranslationCompleted { get; set; }
    }
}
