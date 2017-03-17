using LP.Api.Shared.Interfaces.BusinessLayer.Translation;
using LP.Translation.BusinessLayer;
using LP.Translation.BusinessLayer.Commands;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Translation.IoC
{
    public class TranslationNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAskTranslationApiBusiness>().To<AskTranslationApiBusiness>().InRequestScope();
            Bind<ITranslationCommands>().To<TranslationCommands>().InRequestScope();
        }
    }
}