namespace LP.Host.Integration.Providers
{
    internal static class UriProvider
    {
        public static class Authentication
        {
            public const string Login = "api/authentication/login";
            public const string UserInformation = "api/authentication/user-information";
            public const string NumberOfRegisteredUsers = "api/authentication/number-of-registered-users";
            public const string NumberOfRegisteredUsersForTrainer = "api/authentication/number-of-registered-users/trainer";
            public const string NumberOfRegisteredUsersForRegion = "api/authentication/number-of-registered-users/region/{0}";
            public const string NumberOfRegisteredUsersForCountry = "api/authentication/number-of-registered-users/country/{0}";
        }

        public static class Content
        {
            public const string GlossaryItems = "api/content/glossary-item";
            public const string GlossaryAudio = "api/content/glossary-audio/{0}";
            public const string FeatureAttachmentModal = "api/content/feature-attachment/modal/{0}";
            public const string FeatureAttachmentVideoModal = "api/content/feature-attachment/video-modal/{0}";

        }

        public static class Exams
        {
            public const string OverviewGroupTypeProgress = "api/exams/dashboard/overview-progress";
            public const string OverviewGroupTypeRegionProgress = "api/exams/dashboard/overview-progress/region/{0}";
            public const string OverviewGroupTypeTrainerProgress = "api/exams/dashboard/overview-progress/trainer";
            public const string OverviewGroupTypeCountryProgress = "api/exams/dashboard/overview-progress/country/{0}";
            public const string OverviewCountryGlobalProgress = "api/exams/dashboard/overview-progress/country-statistics";
            public const string OverviewCountryRegionProgress = "api/exams/dashboard/overview-progress/country-statistics/region/{0}";
            public const string OverviewGroupProgress = "api/exams/group/overview-progress/{0}";
            public const string TrainingAreaProgress = "api/exams/training-area/progress/{0}";
            public const string TrainingAreaCompletion = "api/exams/training-area/completion";
            public const string GroupTypeHeaders = "api/exams/dashboard/overview-progress/group-type-headers";
            public const string TrainerActivities = "api/exams/dashboard/trainer-activities";
        }
    }
}
