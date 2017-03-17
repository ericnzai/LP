using LP.Api.Shared.Interfaces.BusinessLayer.Translation;

namespace LP.Translation.BusinessLayer
{
    public class AskTranslationApiBusiness : IAskTranslationApiBusiness
    {
        private readonly ITranslationCommands _translationCommands;

        public AskTranslationApiBusiness(ITranslationCommands translationCommands)
        {
            _translationCommands = translationCommands;
        }

        public ITranslationCommands TranslationCommands
        {
            get { return _translationCommands; }
        }
    }
}
