using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.EntityModels.StoredProcedure.Output;
using LP.Model.Authentication;
using LP.Model.Extensions;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Exams;
using Status = LP.ServiceHost.DataContracts.Enums.Status;

namespace LP.Exams.BusinessLayer.Commands
{
    public class GroupCompletionCommands : IGroupCompletionCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IPercentageCompletionCommands _percentageCompletionCommands;
        private readonly ITrainingAreaCommands _trainingAreaCommands;
        private readonly ILastAreasViewedProvider _lastAreasViewedProvider;
        private readonly ICultureProvider _cultureProvider;
        private readonly ICertificatesAchievedCommands _certificatesAchievedCommands;
        private List<GroupPercentageComplete> _groupsPercentageComplete;
        private List<LastSectionsViewed> _lastSectionsViewed;
        private IQueryable<TrainingsExam> _trainingsExams;
        private IQueryable<ltl_UsersFavouriteGroup> _usersFavouriteGroups;
        private IQueryable<CertificatesAchieved> _certificatesAchieved;

        private const int MinimumPercentageWhenUserHasStartedGroup = 3;

        public GroupCompletionCommands(IBaseCommands baseCommands, IPercentageCompletionCommands percentageCompletionCommands, 
            ITrainingAreaCommands trainingAreaCommands, ILastAreasViewedProvider lastAreasViewedProvider, ICultureProvider cultureProvider, ICertificatesAchievedCommands certificatesAchievedCommands)
        {
            _baseCommands = baseCommands;
            _percentageCompletionCommands = percentageCompletionCommands;
            _trainingAreaCommands = trainingAreaCommands;
            _lastAreasViewedProvider = lastAreasViewedProvider;
            _cultureProvider = cultureProvider;
            _certificatesAchievedCommands = certificatesAchievedCommands;
        }

        public async Task<TrainingAreaProgressResponseContract> GetAllCompleteGroupsForTrainingAreaAsync(int trainingAreaId, UserDetails userDetails)
        {
            _usersFavouriteGroups = await
                _baseCommands.GetConditionalAsync<ltl_UsersFavouriteGroup>(u => u.UserID == userDetails.UserId);

            var groupsAndGroupTypesForTrainingArea = await _trainingAreaCommands.GetTrainingAreaWithAllGroupInfo(trainingAreaId, userDetails.RoleIds, AccessType.DisplayOnDashboards);

            if (groupsAndGroupTypesForTrainingArea == null) return new TrainingAreaProgressResponseContract();

            var trainingArea = groupsAndGroupTypesForTrainingArea.Key;

            var groupsGroupedByGroupType = groupsAndGroupTypesForTrainingArea.Select(grouped => grouped).ToList();

            var groups = groupsGroupedByGroupType.SelectMany(group => group).Where(a => a.StatusBankID == (int)Status.Live).ToList();

            if (groups.Count == 0) return new TrainingAreaProgressResponseContract {TrainingAreaId = trainingAreaId, TrainingAreaName = trainingArea.Name};

            var groupIds = groups.Select(g => g.GroupID).ToList();

            _groupsPercentageComplete = await _percentageCompletionCommands.PercentageAchievedForGroups(userDetails.UserId, groupIds);

            var groupProgressContracts = new List<GroupProgressContract>();

            var trainingAreaFriendlyUrl = trainingArea.FriendlyUrl;

            _lastSectionsViewed = _lastAreasViewedProvider.GetLastSectionsViewed(userDetails.UserId, groups).ToList();

            _trainingsExams = await _baseCommands.GetConditionalWithIncludesAsync<TrainingsExam>(g => groupIds.Contains(g.GroupId) && g.Exam.StatusId == (int)Status.Live, inc => inc.Exam);

            _certificatesAchieved = await _certificatesAchievedCommands.GetCertificatesAchievedForUser(userDetails);

            foreach (var groupTypeWithGroups in groupsGroupedByGroupType)
            {
                var groupProgressContract = ProcessGroupProgressContract(groupTypeWithGroups, userDetails, trainingAreaFriendlyUrl);

                if (groupProgressContract != null) groupProgressContracts.Add(groupProgressContract);
            }

            var trainingAreaProgressResponseContract = new TrainingAreaProgressResponseContract
            {
                TrainingAreaId = trainingAreaId,
                TrainingAreaName = trainingArea.Name,
                GroupProgressContracts = groupProgressContracts
            };

            return trainingAreaProgressResponseContract;
        }

        private Group SelectMainGroup(IGrouping<ltl_GroupType, Group> groupTypeWithGroups, UserDetails userDetails)
        {
            var groupType = groupTypeWithGroups.Key;
     
            var userFavouriteGroup = _usersFavouriteGroups.FirstOrDefault(a => a.GroupTypeID == groupType.ID);

            Group group;

            if (userFavouriteGroup != null)
            {
                group = groupTypeWithGroups.FirstOrDefault(a => a.GroupID == userFavouriteGroup.FavouriteGroupID);

                if (group != null) return group;
            }

            group = groupTypeWithGroups.FirstOrDefault(a => a.Culture == userDetails.CurrentCulture);

            if (group != null) return group;

            return groupTypeWithGroups.First(a => a.Culture == _cultureProvider.DefaultCultureString);
        }

        private CultureInfo GetCultureInfoFromGroupEntity(Group group)
        {
            var culture = group.Culture;

            return _cultureProvider.GetCultureInfoWithDefault(culture);
        }

        private GroupProgressContract ProcessGroupProgressContract(IGrouping<ltl_GroupType, Group> groupTypeWithGroups, UserDetails userDetails, string trainingAreaFriendlyUrl)
        {
            var groupEntity = SelectMainGroup(groupTypeWithGroups, userDetails);

            var numberOfChapters = _trainingsExams.Where(g => g.GroupId == groupEntity.GroupID && g.IsLive).GroupBy(g => g.ParentSectionId).Count();

            if (numberOfChapters == 0) return null;

            var cultureInfo = GetCultureInfoFromGroupEntity(groupEntity);

            var languageName = cultureInfo.IsNeutralCulture ? cultureInfo.DisplayName : cultureInfo.Parent.DisplayName;

            var percentageComplete = 0;

            var trainingStatus = TrainingStatus.NotStarted;
            
            //var chapterButtonText = "START TRAINING";
            var groupUrl = groupEntity.FriendlyUrl.ToGroupUrl(trainingAreaFriendlyUrl);
            var currentChapter = string.Empty;
            var chapterUrl = groupUrl;

            var groupPercentageComplete =
                _groupsPercentageComplete.FirstOrDefault(g => g.GroupId == groupEntity.GroupID);

            if (groupPercentageComplete != null)
            {
                percentageComplete = groupPercentageComplete.PercentageComplete;
            }
            var hasCertificate = _certificatesAchieved.Any(c => c.GroupId == groupEntity.GroupID);

            var lastSectionsViewedForGroup = _lastSectionsViewed.Where(g => g.GroupId == groupEntity.GroupID).OrderByDescending(a => a.DateTime);

            var lastSectionViewed = lastSectionsViewedForGroup.FirstOrDefault();

            if (percentageComplete > 0)
            {
                trainingStatus = percentageComplete == 100 ? TrainingStatus.Completed : TrainingStatus.InProgress;
            }

            if (lastSectionViewed != null)
            {
                chapterUrl = lastSectionViewed.FriendlyUrl.Replace("~/", "");
                currentChapter = lastSectionViewed.Subject;
                trainingStatus = percentageComplete == 100 ? TrainingStatus.Completed : TrainingStatus.InProgress; // "REVIEW TRAINING" :  "CONTINUE TRAINING"
                if (percentageComplete == 0) percentageComplete = MinimumPercentageWhenUserHasStartedGroup;
            }
            
            var groupProgressContract = new GroupProgressContract
            {
                GroupName = groupEntity.Name,
                GroupId = groupEntity.GroupID,
                Culture = groupEntity.Culture,
                LanguageName = languageName,
                GroupUrl = groupUrl,
                PercentageComplete = percentageComplete,
                NumberOfChapters = numberOfChapters,
                TrainingStatus = trainingStatus,
                CurrentChapter = currentChapter,
                ChapterUrl = chapterUrl,
                WasCertified = hasCertificate
            };

            if (groupEntity.StatusBankID.HasValue)
            {
                groupProgressContract.GroupStatus = (Status)groupEntity.StatusBankID.Value;
            }

            return groupProgressContract;
        }
    }
}
