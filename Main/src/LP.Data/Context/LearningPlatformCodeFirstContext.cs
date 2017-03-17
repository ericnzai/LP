using System.Data.Entity;
using LP.Data.ViewMappings;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.EntityModels.Views;

namespace LP.Data.Context
{
    public class LearningPlatformCodeFirstContext : DbContext
    {
        public LearningPlatformCodeFirstContext()
            : base("name=LearningPlatformCodeFirstConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<askCore_AuthenticationType> askCore_AuthenticationType { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<askCore_Country_CulturePermissions> askCore_Country_CulturePermissions { get; set; }
        public virtual DbSet<askCore_FieldDefinition> askCore_FieldDefinition { get; set; }
        public virtual DbSet<askCore_FieldType> askCore_FieldType { get; set; }
        public virtual DbSet<askCore_PreRegisterRoleAssignments> askCore_PreRegisterRoleAssignments { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<askCore_RoleGroup> askCore_RoleGroup { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<askCore_SettingsType> askCore_SettingsType { get; set; }
        public virtual DbSet<askCore_SiteSettings> askCore_SiteSettings { get; set; }
        public virtual DbSet<askCore_SubscriptionExtraInfo> askCore_SubscriptionExtraInfo { get; set; }
        public virtual DbSet<askCore_Subscriptions> askCore_Subscriptions { get; set; }
        public virtual DbSet<askCore_UserDetails> askCore_UserDetails { get; set; }
        public virtual DbSet<askCore_UserExtraInfo> askCore_UserExtraInfo { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<askCore_UserStatus> askCore_UserStatus { get; set; }
        public virtual DbSet<askQuickCM> askQuickCMS { get; set; }
        public virtual DbSet<ImageModulePostBlockPopup> ImageModulePostBlockPopups { get; set; }
        public virtual DbSet<ImageModulePostBlock> ImageModulePostBlocks { get; set; }
        public virtual DbSet<ImageModulePostPopoutLayoutType> ImageModulePostPopoutLayoutTypes { get; set; }
        public virtual DbSet<ImagingModulePost> ImagingModulePosts { get; set; }
        public virtual DbSet<AdminHistory> ltl_AdminHistory { get; set; }
        public virtual DbSet<AutomaticRoleAssignment> ltl_AutomaticRoleAssignment { get; set; }
        public virtual DbSet<ltl_AutomaticRoleAssignmentType> ltl_AutomaticRoleAssignmentType { get; set; }
        public virtual DbSet<ClientApp> ltl_ClientApp { get; set; }
        public virtual DbSet<ltl_ClientAppFeatureAttachmentVisiblity> ltl_ClientAppFeatureAttachmentVisiblity { get; set; }
        public virtual DbSet<ltl_ClientAppPostVisiblity> ltl_ClientAppPostVisiblity { get; set; }
        public virtual DbSet<ltl_CultureRole_CultureString> ltl_CultureRole_CultureString { get; set; }
        public virtual DbSet<ltl_DraftStatusType> ltl_DraftStatusType { get; set; }
        public virtual DbSet<ltl_DynamicRoles> ltl_DynamicRoles { get; set; }
        public virtual DbSet<ltl_EmailTemplate> ltl_EmailTemplate { get; set; }
        public virtual DbSet<ltl_EmbeddedContent> ltl_EmbeddedContent { get; set; }
        public virtual DbSet<ltl_ExtendedResults> ltl_ExtendedResults { get; set; }
        public virtual DbSet<ltl_Favourites> ltl_Favourites { get; set; }
        public virtual DbSet<ltl_FeatureAttachment> ltl_FeatureAttachment { get; set; }
        public virtual DbSet<ltl_FeatureAttachment_CustomField> ltl_FeatureAttachment_CustomField { get; set; }
        public virtual DbSet<ltl_FeatureAttachment_CustomFieldDefinition> ltl_FeatureAttachment_CustomFieldDefinition { get; set; }
        public virtual DbSet<ltl_FeatureAttachment_CustomFieldType> ltl_FeatureAttachment_CustomFieldType { get; set; }
        public virtual DbSet<ltl_FeatureAttachmentCategory> ltl_FeatureAttachmentCategory { get; set; }
        public virtual DbSet<ltl_FeatureAttachmentTranslation> ltl_FeatureAttachmentTranslation { get; set; }
        public virtual DbSet<ltl_FeatureAttachmentType> ltl_FeatureAttachmentType { get; set; }
        public virtual DbSet<ltl_GroupCategory> ltl_GroupCategory { get; set; }
        public virtual DbSet<ltl_GroupCertificates> ltl_GroupCertificates { get; set; }
        public virtual DbSet<GroupPermission> ltl_GroupPermissions { get; set; }
        public virtual DbSet<Group> ltl_Groups { get; set; }
        public virtual DbSet<ltl_GroupType> ltl_GroupType { get; set; }
        public virtual DbSet<ltl_GroupTypeTranslations> ltl_GroupTypeTranslations { get; set; }
        public virtual DbSet<ltl_History> ltl_History { get; set; }
        public virtual DbSet<ltl_HistoryType> ltl_HistoryType { get; set; }
        public virtual DbSet<ltl_HoverOver> ltl_HoverOver { get; set; }
        public virtual DbSet<ltl_HoverOverAudio> ltl_HoverOverAudio { get; set; }
        public virtual DbSet<ltl_HoverOverAudioCulture> ltl_HoverOverAudioCulture { get; set; }
        public virtual DbSet<ltl_HoverOverSettings> ltl_HoverOverSettings { get; set; }
        public virtual DbSet<ltl_ItemDescription> ltl_ItemDescription { get; set; }
        public virtual DbSet<ltl_Link> ltl_Link { get; set; }
        public virtual DbSet<ltl_Link_Role> ltl_Link_Role { get; set; }
        public virtual DbSet<ltl_MandatoryRolePerCulture> ltl_MandatoryRolePerCulture { get; set; }
        public virtual DbSet<ltl_Navigation> ltl_Navigation { get; set; }
        public virtual DbSet<ltl_NavigationCulture> ltl_NavigationCulture { get; set; }
        public virtual DbSet<ltl_NavigationRole> ltl_NavigationRole { get; set; }
        public virtual DbSet<News> ltl_News { get; set; }
        public virtual DbSet<ltl_NewsCategory> ltl_NewsCategory { get; set; }
        public virtual DbSet<ltl_OldIndication_EmploymentRole> ltl_OldIndication_EmploymentRole { get; set; }
        public virtual DbSet<ltl_Posts> ltl_Posts { get; set; }
        public virtual DbSet<ltl_PostTranslationDrafts> ltl_PostTranslationDrafts { get; set; }
        public virtual DbSet<ltl_PostTranslationMapping> ltl_PostTranslationMapping { get; set; }
        public virtual DbSet<ltl_PostTranslations> ltl_PostTranslations { get; set; }
        public virtual DbSet<ltl_ScormPackage> ltl_ScormPackage { get; set; }
        public virtual DbSet<ltl_SectionPermissions> ltl_SectionPermissions { get; set; }
        public virtual DbSet<ltl_Sections> ltl_Sections { get; set; }
        public virtual DbSet<ltl_SectionTranslationDrafts> ltl_SectionTranslationDrafts { get; set; }
        public virtual DbSet<ltl_SectionTranslationMapping> ltl_SectionTranslationMapping { get; set; }
        public virtual DbSet<ltl_SectionTranslationNotifications> ltl_SectionTranslationNotifications { get; set; }
        public virtual DbSet<ltl_SectionTranslations> ltl_SectionTranslations { get; set; }
        public virtual DbSet<ltl_SettingsType> ltl_SettingsType { get; set; }
        public virtual DbSet<ltl_SiteSettings> ltl_SiteSettings { get; set; }
        public virtual DbSet<ltl_SSOLog> ltl_SSOLog { get; set; }
        public virtual DbSet<ltl_StatusBank> ltl_StatusBank { get; set; }
        public virtual DbSet<ltl_StickyNotes> ltl_StickyNotes { get; set; }
        public virtual DbSet<ltl_SupportedCulture> ltl_SupportedCulture { get; set; }
        public virtual DbSet<TrainingArea> ltl_TrainingArea { get; set; }
        public virtual DbSet<ltl_TrainingAreaPermissions> ltl_TrainingAreaPermissions { get; set; }
        public virtual DbSet<ltl_TranslationHistory> ltl_TranslationHistory { get; set; }
        public virtual DbSet<ltl_TranslationHistoryData> ltl_TranslationHistoryData { get; set; }
        public virtual DbSet<ltl_TranslationNotificationActions> ltl_TranslationNotificationActions { get; set; }
        public virtual DbSet<ltl_UserGroupActivity> ltl_UserGroupActivity { get; set; }
        public virtual DbSet<ltl_UserHistory> ltl_UserHistory { get; set; }
        public virtual DbSet<ltl_UserPostViewed> ltl_UserPostViewed { get; set; }
        public virtual DbSet<ltl_UsersFavouriteGroup> ltl_UsersFavouriteGroup { get; set; }
        public virtual DbSet<ltl_UserSurvey> ltl_UserSurvey { get; set; }
        public virtual DbSet<ltl_UserSurveyOptions> ltl_UserSurveyOptions { get; set; }
        public virtual DbSet<ltl_VersionHistory> ltl_VersionHistory { get; set; }
        public virtual DbSet<quiz_AnswerBank> quiz_AnswerBank { get; set; }
        public virtual DbSet<quiz_Attempt> quiz_Attempt { get; set; }
        public virtual DbSet<quiz_Category> quiz_Category { get; set; }
        public virtual DbSet<quiz_CBMValue> quiz_CBMValue { get; set; }
        public virtual DbSet<quiz_CommentBank> quiz_CommentBank { get; set; }
        public virtual DbSet<quiz_ExamBank> quiz_ExamBank { get; set; }
        public virtual DbSet<quiz_ExamQuestions> quiz_ExamQuestions { get; set; }
        public virtual DbSet<quiz_OnlyBCCResultsForTheseEmails> quiz_OnlyBCCResultsForTheseEmails { get; set; }
        public virtual DbSet<quiz_PostExams> quiz_PostExams { get; set; }
        public virtual DbSet<quiz_QuestionBank> quiz_QuestionBank { get; set; }
        public virtual DbSet<quiz_QuestionOrder> quiz_QuestionOrder { get; set; }
        public virtual DbSet<quiz_QuestionType> quiz_QuestionType { get; set; }
        public virtual DbSet<quiz_QuizSettings> quiz_QuizSettings { get; set; }
        public virtual DbSet<quiz_ResponseBank> quiz_ResponseBank { get; set; }
        public virtual DbSet<quiz_RolesBank> quiz_RolesBank { get; set; }
        public virtual DbSet<quiz_SettingsType> quiz_SettingsType { get; set; }
        public virtual DbSet<quiz_StatusBank> quiz_StatusBank { get; set; }
        public virtual DbSet<quiz_StudentTrainer> quiz_StudentTrainer { get; set; }
        public virtual DbSet<ResourceLocalization> Resource_Localization { get; set; }

        public virtual DbSet<Attempt> Attempts { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificatesAchieved> CertificatesAchieved { get; set; }
        public virtual DbSet<TrainingsExam> TrainingsExams { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AttemptDetail> AttemptDetails { get; set; }
        public virtual DbSet<CertificatesAchievedExam> CertificatesAchievedExams { get; set; }
        public virtual DbSet<ExamDetail> ExamDetails { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamsQuestion> ExamsQuestions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionsOrder> QuestionsOrders { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<ResponsesAnswer> ResponsesAnswers { get; set; }
        //public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<TopicCategoryTranslation> TopicCategoryTranslations { get; set; }
        public virtual DbSet<TopicTranslation> TopicTranslations { get; set; }
        public virtual DbSet<TopicCategory> TopicCategories { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<VAConversionTool> VAConversionTools { get; set; }
        //public virtual DbSet<VAConversionToolNotification> VAConversionToolNotifications { get; set; }
        //public virtual DbSet<VAConversionToolTranslation> VAConversionToolTranslations { get; set; }
            
        public virtual DbSet<TrainersWithStudentsCountries> TrainersWithStudentsCountries { get; set; }
        public virtual DbSet<GroupTypesWithUsers> GroupTypesWithUsers { get; set; }
            
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                 .Property(e => e.LastUpdated)
                 .HasPrecision(2);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.ResponsesAnswers)
                .WithRequired(e => e.Answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.AttemptStarted)
                .HasPrecision(2);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.AttemptFinished)
                .HasPrecision(2);

            modelBuilder.Entity<Attempt>()
                .Property(e => e.DateModified)
                .HasPrecision(2);

            modelBuilder.Entity<Attempt>()
                .HasOptional(e => e.AttemptDetail)
                .WithRequired(e => e.Attempt);

            modelBuilder.Entity<Attempt>()
                .HasOptional(e => e.CertificatesAchieved)
                .WithRequired(e => e.Attempt);

            modelBuilder.Entity<Attempt>()
                .HasMany(e => e.QuestionsOrders)
                .WithRequired(e => e.Attempt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attempt>()
                .HasMany(e => e.Responses)
                .WithRequired(e => e.Attempt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Certificate>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<Certificate>()
                .Property(e => e.Filename)
                .IsUnicode(false);

            modelBuilder.Entity<Certificate>()
                .HasMany(e => e.CertificatesAchieveds)
                .WithRequired(e => e.Certificate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CertificatesAchieved>()
                .Property(e => e.DateTimeAchieved)
                .HasPrecision(2);

            modelBuilder.Entity<CertificatesAchieved>()
                .HasMany(e => e.CertificatesAchievedExams)
                .WithRequired(e => e.CertificatesAchieved)
                .HasForeignKey(e => e.CertificateAchievedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.LastUpdated)
                .HasPrecision(2);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Attempts)
                .WithRequired(e => e.Exam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.CertificatesAchievedExams)
                .WithRequired(e => e.Exam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .HasOptional(e => e.ExamDetail)
                .WithRequired(e => e.Exam);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.ExamsQuestions)
                .WithRequired(e => e.Exam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.TrainingsExams)
                .WithRequired(e => e.Exam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuestionCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionCategory>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.QuestionCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.LastUpdated)
                .HasPrecision(2);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.ExamsQuestions)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionsOrders)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Responses)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuestionType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<QuestionType>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.QuestionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Response>()
                .HasMany(e => e.ResponsesAnswers)
                .WithRequired(e => e.Respons)
                .HasForeignKey(e => e.ResponseId)
                .WillCascadeOnDelete(false);

           
            modelBuilder.Entity<askCore_AuthenticationType>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.CountryCode)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_FieldDefinition>()
                .Property(e => e.FieldName)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_FieldDefinition>()
                .HasMany(e => e.askCore_SubscriptionExtraInfo)
                .WithRequired(e => e.askCore_FieldDefinition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<askCore_FieldDefinition>()
                .HasMany(e => e.askCore_UserExtraInfo)
                .WithRequired(e => e.askCore_FieldDefinition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<askCore_FieldType>()
                .Property(e => e.FieldType)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_FieldType>()
                .HasMany(e => e.askCore_FieldDefinition)
                .WithRequired(e => e.askCore_FieldType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_RoleGroup>()
                .Property(e => e.RoleGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.askCore_Country_CulturePermissions)
                .WithRequired(e => e.askCore_Roles)
                .HasForeignKey(e => e.CultureRoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.askCore_UsersRoles)
                .WithRequired(e => e.askCore_Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.ltl_AutomaticRoleAssignment)
                .WithRequired(e => e.askCore_Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.askCore_Roles1)
                .WithMany(e => e.askCore_Roles2)
                .Map(m => m.ToTable("askCore_Role_ChildRole").MapLeftKey("RoleID").MapRightKey("ChildRoleID"));

            modelBuilder.Entity<askCore_SettingsType>()
                .Property(e => e.SettingType)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_SettingsType>()
                .HasMany(e => e.askCore_SiteSettings)
                .WithRequired(e => e.askCore_SettingsType)
                .HasForeignKey(e => e.SettingTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<askCore_SiteSettings>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_SiteSettings>()
                .Property(e => e.SettingValue)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_SiteSettings>()
                .Property(e => e.PossibleValues)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_SubscriptionExtraInfo>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_Subscriptions>()
                .HasMany(e => e.askCore_SubscriptionExtraInfo)
                .WithRequired(e => e.askCore_Subscriptions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.JobTitle)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Institution)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.HomePhone)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserDetails>()
                .Property(e => e.FaxNumber)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserExtraInfo>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserExtraInfo>()
                .Property(e => e.FileType)
                .IsUnicode(false);

            modelBuilder.Entity<askCore_UserExtraInfo>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.askCore_UserDetails)
                .WithRequired(e => e.askCore_Users);

            modelBuilder.Entity<User>()
                .HasMany(e => e.askCore_UserExtraInfo)
                .WithRequired(e => e.askCore_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.ltl_UserHistory)
                .WithRequired(e => e.askCore_Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.askCore_Users1)
                .WithOptional(e => e.askCore_Users2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.askCore_UsersRoles)
                .WithRequired(e => e.askCore_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ltl_UsersFavouriteGroup)
                .WithRequired(e => e.askCore_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<askCore_UserStatus>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<askQuickCM>()
                .Property(e => e.ContentName)
                .IsUnicode(false);

            modelBuilder.Entity<askQuickCM>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ImageModulePostBlock>()
                .HasMany(e => e.ImageModulePostBlockPopups)
                .WithRequired(e => e.ImageModulePostBlock)
                .HasForeignKey(e => e.ImageModulePostBlock_ImageModulePostBlockId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImagingModulePost>()
                .HasMany(e => e.ImageModulePostBlocks)
                .WithRequired(e => e.ImagingModulePost)
                .HasForeignKey(e => e.ImagingModulePost_ImagingModulePostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdminHistory>()
                .Property(e => e.Action)
                .IsUnicode(false);

            modelBuilder.Entity<AdminHistory>()
                .Property(e => e.Notes1)
                .IsUnicode(false);

            modelBuilder.Entity<AdminHistory>()
                .Property(e => e.Notes2)
                .IsUnicode(false);

            modelBuilder.Entity<AdminHistory>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<AutomaticRoleAssignment>()
                .Property(e => e.AutomaticRoleAssignmentValue)
                .IsUnicode(false);

            modelBuilder.Entity<AutomaticRoleAssignment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_AutomaticRoleAssignmentType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_AutomaticRoleAssignmentType>()
                .HasMany(e => e.ltl_AutomaticRoleAssignment)
                .WithRequired(e => e.ltl_AutomaticRoleAssignmentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientApp>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ClientApp>()
                .Property(e => e.Platform)
                .IsUnicode(false);

            modelBuilder.Entity<ClientApp>()
                .HasMany(e => e.ltl_ClientAppFeatureAttachmentVisiblity)
                .WithRequired(e => e.ClientApp)
                .HasForeignKey(e => e.ClientAppID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientApp>()
                .HasMany(e => e.ltl_ClientAppPostVisiblity)
                .WithRequired(e => e.ClientApp)
                .HasForeignKey(e => e.ClientAppID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_CultureRole_CultureString>()
                .Property(e => e.CultureString)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_DraftStatusType>()
                .Property(e => e.DraftStatus)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_DraftStatusType>()
                .HasMany(e => e.ltl_PostTranslationDrafts)
                .WithRequired(e => e.ltl_DraftStatusType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_DraftStatusType>()
                .HasMany(e => e.ltl_SectionTranslationDrafts)
                .WithRequired(e => e.ltl_DraftStatusType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_EmbeddedContent>()
                .Property(e => e.FolderName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_ExtendedResults>()
                .Property(e => e.UserCulture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_ExtendedResults>()
                .Property(e => e.ManagerUserName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_ExtendedResults>()
                .Property(e => e.ManagerCulture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_ExtendedResults>()
                .Property(e => e.ManagerDisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachment>()
                .HasMany(e => e.ltl_ClientAppFeatureAttachmentVisiblity)
                .WithRequired(e => e.ltl_FeatureAttachment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_FeatureAttachment>()
                .HasMany(e => e.ltl_FeatureAttachment_CustomField)
                .WithOptional(e => e.ltl_FeatureAttachment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ltl_FeatureAttachment_CustomFieldDefinition>()
                .Property(e => e.CustomFieldName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachment_CustomFieldType>()
                .Property(e => e.CustomFieldType)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachmentCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachmentCategory>()
                .HasMany(e => e.ltl_FeatureAttachment)
                .WithOptional(e => e.ltl_FeatureAttachmentCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<ltl_FeatureAttachmentType>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachmentType>()
                .Property(e => e.Extra)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_FeatureAttachmentType>()
                .HasMany(e => e.ltl_FeatureAttachment)
                .WithRequired(e => e.ltl_FeatureAttachmentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_GroupCertificates>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_GroupCertificates>()
                .Property(e => e.Filename)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_GroupType>()
                .HasMany(e => e.ltl_Groups)
                .WithOptional(e => e.ltl_GroupType)
                .HasForeignKey(e => e.GroupTypeID);

            modelBuilder.Entity<ltl_GroupType>()
                .HasMany(e => e.ltl_GroupTypeTranslations)
                .WithRequired(e => e.ltl_GroupType)
                .HasForeignKey(e => e.GroupTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_GroupType>()
                .HasMany(e => e.ltl_Link)
                .WithOptional(e => e.ltl_GroupType)
                .HasForeignKey(e => e.GroupTypeID);

            modelBuilder.Entity<ltl_GroupType>()
                .HasMany(e => e.ltl_UsersFavouriteGroup)
                .WithRequired(e => e.ltl_GroupType)
                .HasForeignKey(e => e.GroupTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_History>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_History>()
                .Property(e => e.BrowserDetails)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_History>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_HistoryType>()
                .Property(e => e.HistoryType)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_HoverOver>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_HoverOverAudio>()
                .HasMany(e => e.ltl_HoverOver)
                .WithOptional(e => e.ltl_HoverOverAudio)
                .HasForeignKey(e => e.AudioFileID);

            modelBuilder.Entity<ltl_HoverOverAudioCulture>()
                .Property(e => e.hoac_Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_HoverOverSettings>()
                .Property(e => e.Setting)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Link>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Link>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Link>()
                .Property(e => e.CultureVersion)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Link>()
                .HasMany(e => e.ltl_Link_Role)
                .WithRequired(e => e.ltl_Link)
                .HasForeignKey(e => e.LinkID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_MandatoryRolePerCulture>()
                .Property(e => e.CultureCode)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Navigation>()
                .HasMany(e => e.ltl_NavigationCulture)
                .WithRequired(e => e.ltl_Navigation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Navigation>()
                .HasMany(e => e.ltl_NavigationRole)
                .WithRequired(e => e.ltl_Navigation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_NavigationCulture>()
                .Property(e => e.Culture)
                .IsFixedLength();

            modelBuilder.Entity<ltl_NewsCategory>()
                .Property(e => e.UrlName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_NewsCategory>()
                .HasMany(e => e.ltl_News)
                .WithRequired(e => e.ltl_NewsCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.ltl_ClientAppPostVisiblity)
                .WithRequired(e => e.ltl_Posts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.ltl_Favourites)
                .WithOptional(e => e.ltl_Posts)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.ltl_FeatureAttachment)
                .WithOptional(e => e.ltl_Posts)
                .HasForeignKey(e => e.CSPostID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ltl_Posts>()
                .HasOptional(e => e.ltl_ScormPackage)
                .WithRequired(e => e.ltl_Posts);

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.ltl_PostTranslationMapping)
                .WithRequired(e => e.ltl_Posts)
                .HasForeignKey(e => e.DestinationPostId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.quiz_PostExams)
                .WithOptional(e => e.ltl_Posts)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ltl_Posts>()
                .HasMany(e => e.ltl_StickyNotes)
                .WithRequired(e => e.ltl_Posts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Posts>()
                .HasMany<Topic>(s => s.ltl_Topics)
                .WithMany(c => c.Posts)
                .Map(cs =>
                {
                    cs.MapLeftKey("PostID");
                    cs.MapRightKey("TopicID");
                    cs.ToTable("ltl_PostsTopics");
                });

            modelBuilder.Entity<ltl_ScormPackage>()
                .Property(e => e.ScormCourseID)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Sections>()
                .Property(e => e.FriendlyUrl)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Sections>()
                .HasMany(e => e.ltl_PostTranslationMapping)
                .WithRequired(e => e.ltl_Sections)
                .HasForeignKey(e => e.DestinationSectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Sections>()
                .HasMany(e => e.ltl_SectionTranslationNotifications)
                .WithRequired(e => e.ltl_Sections)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_Sections>()
                .HasMany(e => e.ltl_SectionTranslationMapping)
                .WithRequired(e => e.ltl_Sections)
                .HasForeignKey(e => e.SourceSectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_SectionTranslationNotifications>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_SectionTranslations>()
                .HasMany(e => e.ltl_SectionTranslationDrafts)
                .WithOptional(e => e.ltl_SectionTranslations)
                .HasForeignKey(e => new { e.SectionID, e.Culture });

            modelBuilder.Entity<ltl_SectionTranslations>()
                .HasMany(e => e.ltl_SectionTranslationMapping)
                .WithRequired(e => e.ltl_SectionTranslations)
                .HasForeignKey(e => new { e.DestinationSectionId, e.DestinationCulture })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_SettingsType>()
                .Property(e => e.SettingType)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_SettingsType>()
                .HasMany(e => e.ltl_SiteSettings)
                .WithRequired(e => e.ltl_SettingsType)
                .HasForeignKey(e => e.SettingTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_SSOLog>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_StatusBank>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_StatusBank>()
                .HasMany(e => e.ltl_News)
                .WithRequired(e => e.ltl_StatusBank)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_StickyNotes>()
                .Property(e => e.StickyNoteID)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_SupportedCulture>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_SupportedCulture>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_TranslationHistory>()
                .Property(e => e.TranslationType)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_TranslationHistory>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_TranslationHistory>()
                .HasMany(e => e.ltl_TranslationHistoryData)
                .WithRequired(e => e.ltl_TranslationHistory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_TranslationHistoryData>()
                .Property(e => e.FieldName)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_TranslationNotificationActions>()
                .Property(e => e.TranslationNotificationAction)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_UserGroupActivity>()
                .Property(e => e.uga_StartDateTime)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserGroupActivity>()
                .Property(e => e.uga_LastActiveDatetime)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserGroupActivity>()
                .Property(e => e.uga_Modified)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserHistory>()
                .Property(e => e.UserHistory_FirstLogin)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserHistory>()
                .Property(e => e.UserHistory_LastActivity)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserPostViewed>()
                .Property(e => e.upv_DateTime)
                .HasPrecision(2);

            modelBuilder.Entity<ltl_UserSurveyOptions>()
                .Property(e => e.UserSurveyOption)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_UserSurveyOptions>()
                .HasMany(e => e.ltl_UserSurvey)
                .WithRequired(e => e.ltl_UserSurveyOptions)
                .HasForeignKey(e => e.SurveyDecision)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ltl_VersionHistory>()
                .Property(e => e.ScriptName)
                .IsFixedLength();

            modelBuilder.Entity<quiz_AnswerBank>()
                .HasMany(e => e.quiz_AnswerBank1)
                .WithOptional(e => e.quiz_AnswerBank2)
                .HasForeignKey(e => e.TranslatedFromID);

            modelBuilder.Entity<quiz_Attempt>()
                .HasMany(e => e.quiz_ResponseBank)
                .WithRequired(e => e.quiz_Attempt)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_Category>()
                .HasMany(e => e.quiz_Category1)
                .WithOptional(e => e.quiz_Category2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<quiz_Category>()
                .HasMany(e => e.quiz_QuestionBank)
                .WithRequired(e => e.quiz_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_CommentBank>()
                .HasMany(e => e.quiz_ResponseBank)
                .WithOptional(e => e.quiz_CommentBank)
                .HasForeignKey(e => e.CommentID);

            modelBuilder.Entity<quiz_ExamBank>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_ExamBank>()
                .HasMany(e => e.quiz_Attempt)
                .WithRequired(e => e.quiz_ExamBank)
                .HasForeignKey(e => e.ExamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_ExamBank>()
                .HasMany(e => e.quiz_ExamBank1)
                .WithOptional(e => e.quiz_ExamBank2)
                .HasForeignKey(e => e.TranslatedFromID);

            modelBuilder.Entity<quiz_ExamBank>()
                .HasMany(e => e.quiz_ExamQuestions)
                .WithRequired(e => e.quiz_ExamBank)
                .HasForeignKey(e => e.ExamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_ExamBank>()
                .HasMany(e => e.quiz_QuestionOrder)
                .WithOptional(e => e.quiz_ExamBank)
                .HasForeignKey(e => e.ExamID);

            modelBuilder.Entity<quiz_OnlyBCCResultsForTheseEmails>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_QuestionBank>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_QuestionBank>()
                .Property(e => e.Culture)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_QuestionBank>()
                .HasMany(e => e.quiz_AnswerBank)
                .WithRequired(e => e.quiz_QuestionBank)
                .HasForeignKey(e => e.QuestionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_QuestionBank>()
                .HasMany(e => e.quiz_ExamQuestions)
                .WithOptional(e => e.quiz_QuestionBank)
                .HasForeignKey(e => e.QuestionID);

            modelBuilder.Entity<quiz_QuestionBank>()
                .HasMany(e => e.quiz_QuestionBank1)
                .WithOptional(e => e.quiz_QuestionBank2)
                .HasForeignKey(e => e.TranslatedFromID);

            modelBuilder.Entity<quiz_QuestionBank>()
                .HasMany(e => e.quiz_ResponseBank)
                .WithRequired(e => e.quiz_QuestionBank)
                .HasForeignKey(e => e.QuestionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_QuestionOrder>()
                .Property(e => e.QuestionOrder)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_QuestionType>()
                .Property(e => e.QuestionTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_QuestionType>()
                .HasMany(e => e.quiz_QuestionBank)
                .WithRequired(e => e.quiz_QuestionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_QuizSettings>()
                .Property(e => e.SettingName)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_ResponseBank>()
                .Property(e => e.AnswerIDs)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_SettingsType>()
                .Property(e => e.SettingType)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_SettingsType>()
                .HasMany(e => e.quiz_QuizSettings)
                .WithRequired(e => e.quiz_SettingsType)
                .HasForeignKey(e => e.SettingTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_StatusBank>()
                .Property(e => e.StatusName)
                .IsUnicode(false);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_AnswerBank)
                .WithOptional(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_Attempt)
                .WithRequired(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_Category)
                .WithRequired(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_CBMValue)
                .WithOptional(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_CommentBank)
                .WithOptional(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_ExamBank)
                .WithOptional(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_ExamQuestions)
                .WithOptional(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_QuestionBank)
                .WithRequired(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quiz_StatusBank>()
                .HasMany(e => e.quiz_ResponseBank)
                .WithRequired(e => e.quiz_StatusBank)
                .HasForeignKey(e => e.StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResourceLocalization>()
                .Property(e => e.PreviewUrl)
                .IsUnicode(false);

            modelBuilder.Entity<ltl_Posts>().
            HasMany(c => c.ltl_Topics).
            WithMany(p => p.Posts).
            Map(
                m =>
                {
                    m.MapLeftKey("PostID");
                    m.MapRightKey("TopicId");
                    m.ToTable("PostTopics");
                });

            modelBuilder.Configurations.Add(new TrainersWithStudentsCountriesMap());
            modelBuilder.Configurations.Add(new GroupTypesWithUsersMap());
        }
    }
}
