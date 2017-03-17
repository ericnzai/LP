using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;

namespace LP.Content.BusinessLayer
{
    public class AskContentApiBusiness : IAskContentApiBusiness
    {
        private readonly INewsCommands _newsCommands;
        private readonly IGlossaryCommands _glossaryCommands;
        private readonly IGroupCommands _groupCommands;
        private readonly IPdfCommands _pdfCommands;
        private readonly IGlossaryPdfCommands _glossaryPdfCommands;
        private readonly ISearchCommands _searchCommands;
        private readonly ITopicCommands _topicCommands;
        private readonly IFeatureAttachmentCommands _featureAttachmentCommands;
        private readonly ITopicTranslationCommands _topicTranslationCommands;
        private readonly ICultureMenuCommands _cultureMenuCommands;
        private readonly IVAConversionToolCommands _conversionToolCommands;
        private readonly IVAConversionToolTranslationCommands _conversionToolTranslationCommands;
        private readonly IDropdownFilterCommands _dropdownFilterCommands;
        public AskContentApiBusiness(INewsCommands newsCommands, 
            IGlossaryCommands glossaryCommands, 
            IGroupCommands groupCommands, 
            IPdfCommands pdfCommands, 
            IGlossaryPdfCommands glossaryPdfCommands, 
            ISearchCommands searchCommands, 
            ITopicCommands topicCommands, 
            IFeatureAttachmentCommands featureAttachmentCommands,
            ITopicTranslationCommands topicTranslationCommands,
            ICultureMenuCommands cultureMenuCommands, 
            IVAConversionToolCommands conversionToolCommands,
            IVAConversionToolTranslationCommands conversionToolTranslationCommands, 
            IDropdownFilterCommands dropdownFilterCommands)
        {
            _newsCommands = newsCommands;
            _glossaryCommands = glossaryCommands;
            _groupCommands = groupCommands;
            _pdfCommands = pdfCommands;
            _glossaryPdfCommands = glossaryPdfCommands;
            _searchCommands = searchCommands;
            _topicCommands = topicCommands;
            _featureAttachmentCommands = featureAttachmentCommands;
            _topicTranslationCommands = topicTranslationCommands;
            _cultureMenuCommands = cultureMenuCommands;
            _conversionToolCommands = conversionToolCommands;
            _conversionToolTranslationCommands = conversionToolTranslationCommands;
            _dropdownFilterCommands = dropdownFilterCommands;
        }


        public INewsCommands NewsCommands { get { return _newsCommands; } }
        public IGlossaryCommands GlossaryCommands { get { return _glossaryCommands; } }
        public IGroupCommands GroupCommands { get { return _groupCommands; } }
        public IPdfCommands PdfCommands { get { return _pdfCommands; } }
        public IGlossaryPdfCommands GlossaryPdfCommands { get { return _glossaryPdfCommands; } }
        public ISearchCommands SearchCommands { get { return _searchCommands; } }
        public ITopicCommands TopicCommands { get { return _topicCommands; } }
        public IFeatureAttachmentCommands FeatureAttachmentCommands { get { return _featureAttachmentCommands; } }
        public ICultureMenuCommands CultureCommands { get { return _cultureMenuCommands; } }
        public ITopicTranslationCommands TopicTranslationCommands { get { return _topicTranslationCommands; } }
        public IVAConversionToolCommands VaConversionToolCommands { get { return _conversionToolCommands; } }
        public IVAConversionToolTranslationCommands VaConversionToolTranslationCommands { get { return _conversionToolTranslationCommands; } }
        public IDropdownFilterCommands DropdownFilterCommands { get { return _dropdownFilterCommands; } }
    }
}
