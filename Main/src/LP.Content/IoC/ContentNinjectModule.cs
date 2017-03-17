using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content.Filters;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Wrappers;
using LP.Content.BusinessLayer;
using LP.Content.BusinessLayer.Commands;
using LP.Content.BusinessLayer.Filters;
using LP.Content.BusinessLayer.PdfCreation;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Content.IoC
{
    public class ContentNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAskContentApiBusiness>().To<AskContentApiBusiness>().InRequestScope();
            Bind<INewsCommands>().To<NewsCommands>().InRequestScope();
            Bind<IGlossaryCommands>().To<GlossaryCommands>().InRequestScope();
            Bind<IPdfCommands>().To<PdfCommands>().InRequestScope();
            Bind<IGlossaryPdfCommands>().To<GlossaryPdfCommands>().InRequestScope();
            Bind<ISearchCommands>().To<SearchCommands>().InRequestScope();
            Bind<ITopicCommands>().To<TopicCommands>().InRequestScope();
            Bind<ITopicTranslationCommands>().To<TopicTranslationCommands>().InRequestScope();
            Bind<IPdfComponents>().To<PdfComponents>().InRequestScope();
            Bind<IFeatureAttachmentCommands>().To<FeatureAttachmentCommands>().InRequestScope();
            Bind<ICultureMenuCommands>().To<CultureMenuCommands>().InRequestScope();
            Bind<IVAConversionToolCommands>().To<VAConversionToolCommands>().InRequestScope();
            Bind<IVAConversionToolTranslationCommands>().To<VAConversionToolTranslationCommands>().InRequestScope();
            Bind<IPdfContent>().To<PdfContent>().InRequestScope();
            Bind<IFeatureAttachmentFilter>().To<FeatureAttachmentFilter>().InRequestScope();
            Bind<IPostCommands>().To<PostCommands>().InRequestScope();
            Bind<IDropdownFilterCommands>().To<DropdownFilterCommands>().InRequestScope();
        }
    }
}