using LP.Model.ViewModels.Shared;
using LP.Model.ViewModels.VAConversionTool;

namespace LP.Model.ViewModels.VAConversionToolTranslation
{
    public class VAConversionToolTranslationViewModel
    {
        public VAConversionToolTranslationOriginalViewModel OriginalContentViewModel { get; set; }
        public VAConversionToolTranslationNewViewModel NewContentViewModel { get; set; }
        public LanguageSelectorViewModel LanguageSelectorViewModel { get; set; }
        public ConversionToolHistoryViewModel ConversionToolHistoryViewModel { get; set; }
    }
}
