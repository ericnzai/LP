namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IAskExamsApiBusiness
    {
        IPercentageCompletionCommands PercentageCompletionCommands { get; }
        ICertificatesAchievedCommands CertificatesAchievedCommands { get; }
        INumberAchievedCommands NumberAchievedCommands { get; }
        IGroupCompletionCommands GroupCompletionCommands { get; }
        IOverviewGroupTypeProgressCommands OverviewGroupTypeProgressCommands { get; }
        IDashboardDropdownListsCommands DashboardDropdownListsCommands { get; }
        IOverviewCountryProgressCommands OverviewCountryProgressCommands { get; }
        IDashboardActivitiesCommands DashboardActivitiesCommands { get; }
        IOverviewReportCommand OverviewReportCommand { get; }
        
    }
}