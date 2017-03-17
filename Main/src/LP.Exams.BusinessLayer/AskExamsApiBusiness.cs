using LP.Api.Shared.Interfaces.BusinessLayer.Exams;

namespace LP.Exams.BusinessLayer
{
    public class AskExamsApiBusiness : IAskExamsApiBusiness
    {
        private readonly IPercentageCompletionCommands _percentageCompletionCommands;
        private readonly ICertificatesAchievedCommands _certificatesAchievedCommands;
        private readonly INumberAchievedCommands _numberAchievedCommands;
        private readonly IGroupCompletionCommands _groupCompletionCommands;
        private readonly IOverviewGroupTypeProgressCommands _overviewGroupTypeProgressCommands;
        private readonly IDashboardDropdownListsCommands _dashboardDropdownListsCommands;
        private readonly IOverviewCountryProgressCommands _overviewCountryProgressCommands;
        private readonly IDashboardActivitiesCommands _dashboardActivitiesCommands;
        private readonly IOverviewReportCommand _overviewReportCommands; 
        public AskExamsApiBusiness(IPercentageCompletionCommands percentageCompletionCommands,
            ICertificatesAchievedCommands certificatesAchievedCommands, INumberAchievedCommands numberAchievedCommands,IOverviewReportCommand overviewReportRolesCommand,
            IGroupCompletionCommands groupCompletionCommands, IOverviewGroupTypeProgressCommands overviewGroupTypeProgressCommands, IDashboardDropdownListsCommands dashboardDropdownListsCommands, IOverviewCountryProgressCommands overviewCountryProgressCommands, IDashboardActivitiesCommands dashboardActivitiesCommands)
        {
            _percentageCompletionCommands = percentageCompletionCommands;
            _certificatesAchievedCommands = certificatesAchievedCommands;
            _numberAchievedCommands = numberAchievedCommands;
            _groupCompletionCommands = groupCompletionCommands;
            _overviewGroupTypeProgressCommands = overviewGroupTypeProgressCommands;
            _dashboardDropdownListsCommands = dashboardDropdownListsCommands;
            _overviewCountryProgressCommands = overviewCountryProgressCommands;
            _dashboardActivitiesCommands = dashboardActivitiesCommands;
            _overviewReportCommands = overviewReportRolesCommand;
        }

        public IPercentageCompletionCommands PercentageCompletionCommands
        {
            get { return _percentageCompletionCommands; }
        }

        public ICertificatesAchievedCommands CertificatesAchievedCommands
        {
            get { return _certificatesAchievedCommands; }
        }

        public INumberAchievedCommands NumberAchievedCommands
        {
            get { return _numberAchievedCommands; }
        }

        public IGroupCompletionCommands GroupCompletionCommands
        {
            get { return _groupCompletionCommands; }
        }

        public IOverviewGroupTypeProgressCommands OverviewGroupTypeProgressCommands
        {
            get { return _overviewGroupTypeProgressCommands; }
        }

        public IOverviewCountryProgressCommands OverviewCountryProgressCommands
        {
            get { return _overviewCountryProgressCommands; }
        }

        public IDashboardDropdownListsCommands DashboardDropdownListsCommands
        {
            get { return _dashboardDropdownListsCommands; }
        }

        public IDashboardActivitiesCommands DashboardActivitiesCommands
        {
            get { return _dashboardActivitiesCommands; }
        }

        public IOverviewReportCommand OverviewReportCommand
        {
            get { return _overviewReportCommands; }
        }
    }
}
