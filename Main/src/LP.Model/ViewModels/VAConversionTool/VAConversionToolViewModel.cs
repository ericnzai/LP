namespace LP.Model.ViewModels.VAConversionTool
{
    public class VAConversionToolViewModel
    {
        public string Culture { get; set; }
        public string CultureDisplayName { get; set; }
        public string FileName { get; set; }
        public string DownloadFilePath { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public string SaveUrl { get; set; }
        public bool IsNew { get; set; }
        public ConversionToolHistoryViewModel ConversionToolHistoryViewModel { get; set; }
    }
}

