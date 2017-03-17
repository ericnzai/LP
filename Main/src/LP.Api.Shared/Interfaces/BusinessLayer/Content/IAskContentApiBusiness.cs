using LP.Api.Shared.Interfaces.BusinessLayer.Common;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IAskContentApiBusiness
    {
        INewsCommands NewsCommands { get; }
        IGlossaryCommands GlossaryCommands { get; }
        IGroupCommands GroupCommands { get; }
        IPdfCommands PdfCommands { get; }
        IGlossaryPdfCommands GlossaryPdfCommands { get; }
        ISearchCommands SearchCommands { get; }
        ITopicCommands TopicCommands { get; }
        ITopicTranslationCommands TopicTranslationCommands { get; }
        IFeatureAttachmentCommands FeatureAttachmentCommands { get; }
        ICultureMenuCommands CultureCommands { get; }
        IVAConversionToolCommands VaConversionToolCommands { get; }
        IVAConversionToolTranslationCommands VaConversionToolTranslationCommands { get; }
        IDropdownFilterCommands DropdownFilterCommands { get; }
    }
}
