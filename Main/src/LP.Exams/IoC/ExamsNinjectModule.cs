using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Exams.BusinessLayer;
using LP.Exams.BusinessLayer.Commands;
using LP.Exams.BusinessLayer.Filters;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Exams.IoC
{
    public class ExamsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAskExamsApiBusiness>().To<AskExamsApiBusiness>().InRequestScope();
            Bind<IPercentageCompletionCommands>().To<PercentageCompletionCommands>().InRequestScope();
            Bind<ICertificatesAchievedCommands>().To<CertificatesAchievedCommands>().InRequestScope();
            Bind<IExamCommands>().To<ExamCommands>().InRequestScope();
            Bind<IAttemptsCommands>().To<AttemptsCommands>().InRequestScope();
            Bind<INumberAchievedCommands>().To<NumberAchievedCommands>().InRequestScope();
            Bind<IGroupCompletionCommands>().To<GroupCompletionCommands>().InRequestScope();
            Bind<IOverviewGroupTypeProgressCommands>().To<OverviewGroupTypeProgressCommands>().InRequestScope();
            Bind<IOverviewCountryProgressCommands>().To<OverviewCountryProgressCommands>().InRequestScope();
            Bind<IUserPostViewedCommands>().To<UserPostViewedCommands>().InRequestScope();

            Bind<IFilterCertificatesAchieved>().To<FilterCertificatesAchieved>().InRequestScope();
            Bind<IFilterAllowedUser>().To<FilterAllowedUser>().InRequestScope();
            Bind<IFilterAllowedGroups>().To<FilterAllowedGroups>().InRequestScope();
            Bind<IDashboardDropdownListsCommands>().To<DashboardDropdownListsCommands>();
            Bind<IDashboardActivitiesCommands>().To<DashboardActivitiesCommands>();
            Bind<IOverviewReportCommand>().To<OverviewReportCommand>().InRequestScope();
        }
    }
}