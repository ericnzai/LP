namespace LP.Api.Shared.Providers
{
    public static class UriProvider
    {
        public static class Authentication
        {
            public const string Login = "api/authentication/login";
            public const string UserInformation = "api/authentication/user-information";
            public const string NumberOfRegisteredUsers = "api/authentication/number-of-registered-users";
            public const string NumberOfRegisteredUsersForTrainer = "api/authentication/number-of-registered-users/trainer";
            public const string NumberOfRegisteredUsersForRegion = "api/authentication/number-of-registered-users/region/{0}";
            public const string NumberOfRegisteredUsersForCountry = "api/authentication/number-of-registered-users/country/{0}";
            public const string NumberOfRegisteredUsersFiltered = "api/authentication/number-of-registered-users/filtered/{0}/{1}/{2}";
            public const string NumberOfRegisteredUsersFilteredForCountry = "api/authentication/number-of-registered-users/filteredForCountry/{0}/{1}/{2}";
        }

        public static class Content
        {
            public const string GlossaryItems = "api/content/glossary-item";
            public const string GlossaryAudio = "api/content/glossary-audio/{0}";
            public const string FeatureAttachmentModal = "api/content/feature-attachment/modal/{0}";
            public const string FeatureAttachmentVideoModal = "api/content/feature-attachment/video-modal/{0}";
            public const string FeatureAttachmentsList = "api/content/feature-attachment-page";
            public const string DashboardCountryDropdown = "api/content/dashboard-dropdown/country";
            public const string DashboardCountryDropdownWithId = "api/content/dashboard-dropdown/country/{0}";
            public const string DashboardRegionDropdown = "api/content/dashboard-dropdown/region";
            public const string DashboardJobRoleDropdown = "api/content/dashboard-dropdown/job-role";
            public const string DashboardTrainerDropdown = "api/content/dashboard-dropdown/trainer";
            public const string DashboardTrainerDropdownWithId = "api/content/dashboard-dropdown/trainer/{0}";
        }

        public static class Exams
        {
            public const string OverviewGroupTypeProgress = "api/exams/dashboard/overview-progress";
            public const string OverviewGroupTypeProgressFiltered = "api/exams/dashboard/overview-progress-filtered/{0}/{1}/{2}";
            public const string OverviewGroupTypeProgressFilteredForCountry = "api/exams/dashboard/overview-progress-filtered/country/{0}/{1}/{2}";
            public const string OverviewGroupTypeRegionProgress = "api/exams/dashboard/overview-progress/region/{0}";
            public const string OverviewGroupTypeTrainerProgress = "api/exams/dashboard/overview-progress/trainer";
            public const string OverviewGroupTypeCountryProgress = "api/exams/dashboard/overview-progress/country/{0}";
            public const string OverviewCountryGlobalProgress = "api/exams/dashboard/overview-progress/country-statistics";
            public const string OverviewCountryGlobalProgressFiltered = "api/exams/dashboard/overview-progress/country-statistics/{0}";
            public const string OverviewCountryRegionProgress = "api/exams/dashboard/overview-progress/country-statistics/region/{0}";
            public const string OverviewCountryRegionProgressFiltered = "api/exams/dashboard/overview-progress/country-statistics/region/{0}/{1}";
            public const string OverviewGroupProgress = "api/exams/group/overview-progress/{0}";
            public const string TrainingAreaProgress = "api/exams/training-area/progress/{0}";
            public const string TrainingAreaCompletion = "api/exams/training-area/completion";
            public const string GroupTypeHeaders = "api/exams/dashboard/overview-progress/group-type-headers";
            public const string TrainerActivities = "api/exams/dashboard/activities/trainer";
            public const string TrainerActivitiesFiltered = "api/exams/dashboard/activities/trainer/{0}";
            public const string CountryActivities = "api/exams/dashboard/activities/country/{0}";
            public const string CountryActivitiesFiltered = "api/exams/dashboard/activities/country/{0}/{1}";
            public const string OverviewReportRoles = "api/exams/reports-roles-dropdownlist";
        }
    }
}
