namespace LP.ServiceHost.DataContracts.Request.Content
{
    public class VAConversionToolTranslationSaveRequestContract
    {
        public string Culture { get; set; }
        public string FileName { get; set; }
        public string Comments { get; set; }
        public string PermPath { get; set; }
        public string TempPath { get; set; }
        public bool IsTranslationCompleted { get; set; }
    }
}
