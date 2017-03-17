using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Country;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Exams.BusinessLayer.Commands
{
    public class DashboardActivitiesCommands : IDashboardActivitiesCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IUserCommands _userCommands;
        private readonly IUserRoleCommands _userRoleCommands;
        private readonly IGroupTypeCommands _groupTypeCommands;
        private readonly ITrainerCommands _traininerCommands;
        private readonly IAttemptsCommands _attemptsCommands;
        private readonly IUserPostViewedCommands _userPostViewedCommands;
        private readonly IFilterAllowedUser _filterAllowedUser;
        private readonly ICertificatesAchievedCommands _certificatesAchievedCommands;
        private IEnumerable<short> _userPostsViewedGroupedPerUser;
        private List<IGrouping<int, short>> _trainingsExams; 
        private List<IGrouping<int, short>> _allAttempts;
        private List<IGrouping<short, short>> _userPostsViewedGrouped;
        private IEnumerable<CertificatesAchieved> _certificatesAchieved; 
        private IGrouping<int, short> _allUserAttempts;
        private IEnumerable<CertificatesAchieved> _userCertificatesAchieved; 
        public DashboardActivitiesCommands(IBaseCommands baseCommands, ITrainerCommands traininerCommands, 
                                           IUserCommands userCommands, IUserRoleCommands userRoleCommands,
                                           IGroupTypeCommands groupTypeCommands, IAttemptsCommands attemptsCommands, 
                                           IUserPostViewedCommands userPostViewedCommands, IFilterAllowedUser filterAllowedUser,
                                           ICertificatesAchievedCommands certificatesAchievedCommands)
        {
            _baseCommands = baseCommands;
            _userCommands = userCommands;
            _userRoleCommands = userRoleCommands;
            _groupTypeCommands = groupTypeCommands;
            _traininerCommands = traininerCommands;
            _attemptsCommands = attemptsCommands;
            _userPostViewedCommands = userPostViewedCommands;
            _filterAllowedUser = filterAllowedUser;
            _certificatesAchievedCommands = certificatesAchievedCommands;
        }

        private List<Group> _groups;

        public async Task<TrainerActivitiesContract> GetTrainerActivities(int trainerId, string jobRolesIds)
        {
            List<string> jobRoles = jobRolesIds.Split(',').ToList();
            List<int> jobFunctionIds = jobRoles.Select(j => int.Parse(j)).ToList();

            var trainerActivitiesContract = new TrainerActivitiesContract();
            var trainerActivityContracts = new List<TrainerActivityContract>();

            //var traineeUserIds = await _filterAllowedUser.GetUserIdsFilteredByTrainer(trainerId);
            var usersByTrainerAndJobFunctions =
                await _filterAllowedUser.GetUsersFilteredByTrainerAndJobFunctions(trainerId, jobFunctionIds);

            var traineeUserIds = usersByTrainerAndJobFunctions.Select(u => u.UserID).ToList();

            if (!traineeUserIds.Any())
            {
                trainerActivityContracts.Add(new TrainerActivityContract
                {
                    TraineeUserName = "You have no students assigned"
                });
                trainerActivitiesContract.TrainerActivityContract = trainerActivityContracts;

                return trainerActivitiesContract;
            }

            var decryptedUsers = _userCommands.GetDecryptedUsers(traineeUserIds);

            var userRoles = await _userRoleCommands.GetCultureRolesForUsers(traineeUserIds);

            var groups = await _baseCommands.GetAllAsync<Group>();

            _groups = groups.ToList();

            await GetExamsAndAttempts(traineeUserIds);

            await GetCertificatesAchived(traineeUserIds, _groups);

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            foreach (var traineeUserId in traineeUserIds)
            {
                var userId = traineeUserId;

                var userPostsViewedGroupedFirst = _userPostsViewedGrouped.FirstOrDefault(a => a.Key == userId);

                _userPostsViewedGroupedPerUser = new List<short>();

                if (userPostsViewedGroupedFirst != null) _userPostsViewedGroupedPerUser = userPostsViewedGroupedFirst.Select(a => a).ToList();

                _allUserAttempts = _allAttempts.FirstOrDefault(p => p.Key == userId);
                _userCertificatesAchieved = _certificatesAchieved.Where(c => c.UserId == userId);

                var userCultures = GetCultureAndLanguageForUser(userId, userRoles);

                var traineeActivityLanguageContracts = new List<TraineeActivityLanguageContract>();


                foreach (var userCulture in userCultures)
                {
                    var groupActivityContracts = new List<GroupActivityContract>();

                    foreach (var groupType in groupTypes)
                    {
                        var groupActivityContract = new GroupActivityContract
                        {
                            Culture = userCulture.Key,
                            GroupTypeId = groupType.ID,
                            ActivityStatus = GetActivitiesStatus(GetGroupIdsForCulture(groupType.ID, userCulture.Key))
                        };
                        groupActivityContracts.Add(groupActivityContract);
                    }

                    var traineeActivityLanguageContract = new TraineeActivityLanguageContract
                    {
                        Language = userCulture.Value,
                        CultureCode = userCulture.Key,
                        GroupActivityContract = groupActivityContracts
                    };

                    traineeActivityLanguageContracts.Add(traineeActivityLanguageContract);
                }
                var decryptedUserName = decryptedUsers.FirstOrDefault(d => d.UserId == traineeUserId);
                if (decryptedUserName != null)
                {
                    var trainerActivityContract = new TrainerActivityContract
                    {
                        TraineeUserName = decryptedUserName.DecryptedDisplayName,
                        TraineeActivityLanguageContract = traineeActivityLanguageContracts
                    };
                    trainerActivityContracts.Add(trainerActivityContract);
                }
            }

            trainerActivitiesContract.TrainerActivityContract = trainerActivityContracts;

            return trainerActivitiesContract;

        }
        public async Task<TrainerActivitiesContract> GetTrainerActivities(int trainerId)
        {
            var trainerActivitiesContract = new TrainerActivitiesContract();
            var trainerActivityContracts = new List<TrainerActivityContract>();

            var traineeUserIds = await _filterAllowedUser.GetUserIdsFilteredByTrainer(trainerId);
            
            if (!traineeUserIds.Any())
            {
                trainerActivityContracts.Add(new TrainerActivityContract
                {
                      TraineeUserName = "You have no students assigned"
                });
                trainerActivitiesContract.TrainerActivityContract = trainerActivityContracts;

                return trainerActivitiesContract;
            }

            var decryptedUsers = _userCommands.GetDecryptedUsers(traineeUserIds);

            var userRoles = await _userRoleCommands.GetCultureRolesForUsers(traineeUserIds);

            var groups = await _baseCommands.GetAllAsync<Group>();

            _groups = groups.ToList();

            await GetExamsAndAttempts(traineeUserIds);

            await GetCertificatesAchived(traineeUserIds, _groups);

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();
            
            foreach (var traineeUserId in traineeUserIds)
            {
                var userId = traineeUserId;

                var userPostsViewedGroupedFirst = _userPostsViewedGrouped.FirstOrDefault(a => a.Key == userId);

                _userPostsViewedGroupedPerUser = new List<short>();

                if (userPostsViewedGroupedFirst != null)  _userPostsViewedGroupedPerUser = userPostsViewedGroupedFirst.Select(a => a).ToList();
   
                _allUserAttempts = _allAttempts.FirstOrDefault(p => p.Key == userId);
                _userCertificatesAchieved = _certificatesAchieved.Where(c => c.UserId == userId);

                var userCultures = GetCultureAndLanguageForUser(userId, userRoles);
                
                var traineeActivityLanguageContracts = new List<TraineeActivityLanguageContract>();
                

                foreach (var userCulture in userCultures)
                {
                    var groupActivityContracts = new List<GroupActivityContract>();

                    foreach (var groupType in groupTypes)
                    {
                        var groupActivityContract = new GroupActivityContract
                        {
                            Culture = userCulture.Key,
                            GroupTypeId = groupType.ID,
                            ActivityStatus = GetActivitiesStatus(GetGroupIdsForCulture(groupType.ID, userCulture.Key))
                        };
                            groupActivityContracts.Add(groupActivityContract);
                    }

                    var traineeActivityLanguageContract = new TraineeActivityLanguageContract
                    {
                        Language = userCulture.Value, 
                        CultureCode = userCulture.Key,
                        GroupActivityContract = groupActivityContracts
                    };
                    
                    traineeActivityLanguageContracts.Add(traineeActivityLanguageContract);
                }
                var decryptedUserName = decryptedUsers.FirstOrDefault(d=>d.UserId==traineeUserId);
                if (decryptedUserName != null)
                {
                    var trainerActivityContract = new TrainerActivityContract
                    {
                        TraineeUserName = decryptedUserName.DecryptedDisplayName,
                        TraineeActivityLanguageContract = traineeActivityLanguageContracts
                    };
                    trainerActivityContracts.Add(trainerActivityContract);
                }
            }

            trainerActivitiesContract.TrainerActivityContract = trainerActivityContracts;

            return trainerActivitiesContract;
        }

        public async Task<CountryActivitiesContract> GetCountryActivities(int countryId, string jobRolesId)
        {
            List<string> jobRoles = jobRolesId.Split(',').ToList();
            List<int> jobFunctionIds = jobRoles.Select(j => int.Parse(j)).ToList();

            var countryActivitiesContract = new CountryActivitiesContract();
            var countryActivityContracts = new List<CountryActivityContract>();

            var usersByCountryAndJobFunction =
                await _filterAllowedUser.GetUserIdsFilteredByJobFunctions(countryId, jobFunctionIds);

            //var traineeUserIds = await _filterAllowedUser.GetUserIdsFilteredByCountry(countryId);
            var traineeUserIds = usersByCountryAndJobFunction.Select(u => u.UserID).ToList();

            if (!traineeUserIds.Any())
            {
                countryActivityContracts.Add(new CountryActivityContract
                {
                    TraineeUserName = "You have no students assigned"
                });
                countryActivitiesContract.CountryActivityContract = countryActivityContracts;

                return countryActivitiesContract;
            }

         

            var decryptedUsers = _userCommands.GetDecryptedUsers(traineeUserIds);

            var traineeWithTrainerIds = await _filterAllowedUser.GetUserandParentIdsFilteredByCountryId(countryId);

            var trainersIds = await _traininerCommands.GetTrainersIdsByCountryId(countryId);

            trainersIds = usersByCountryAndJobFunction.Where(t => trainersIds.Contains(t.UserID)).Select(t => t.UserID).ToList();
           
            var decryptedTrainers = _userCommands.GetDecryptedUsers(trainersIds);

            var userRoles = await _userRoleCommands.GetCultureRolesForUsers(traineeUserIds);

            var groups = await _baseCommands.GetAllAsync<Group>();

            _groups = groups.ToList();

            await GetExamsAndAttempts(traineeUserIds);
            await GetCertificatesAchived(traineeUserIds, _groups);

            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            foreach (var traineeUserId in traineeUserIds)
            {
                var userId = traineeUserId;

                var userPostsViewedGroupedFirst = _userPostsViewedGrouped.FirstOrDefault(a => a.Key == userId);

                if (userPostsViewedGroupedFirst != null) _userPostsViewedGroupedPerUser = userPostsViewedGroupedFirst.Select(a => a).ToList();

                _allUserAttempts = _allAttempts.FirstOrDefault(p => p.Key == userId);
                _userCertificatesAchieved = _certificatesAchieved.Where(c => c.UserId == userId);

                var userCultures = GetCultureAndLanguageForUser(userId, userRoles);

                var traineeActivityLanguageContracts = new List<TraineeActivityLanguageContract>();


                foreach (var userCulture in userCultures)
                {
                    var groupActivityContracts = new List<GroupActivityContract>();

                    foreach (var groupType in groupTypes)
                    {
                        var groupActivityContract = new GroupActivityContract
                        {
                            Culture = userCulture.Key,
                            GroupTypeId = groupType.ID,
                            ActivityStatus = GetActivitiesStatus(GetGroupIdsForCulture(groupType.ID, userCulture.Key))
                        };
                        groupActivityContracts.Add(groupActivityContract);
                    }

                    var traineeActivityLanguageContract = new TraineeActivityLanguageContract
                    {
                        Language = userCulture.Value,
                        CultureCode = userCulture.Key,
                        GroupActivityContract = groupActivityContracts
                    };

                    traineeActivityLanguageContracts.Add(traineeActivityLanguageContract);
                }

                var decryptedUser = decryptedUsers.FirstOrDefault(d => d.UserId == traineeUserId);
                var trainerId = traineeWithTrainerIds.FirstOrDefault(t => t.Key == traineeUserId).Value;
                var decryptedTrainerName = string.Empty;
                var decryptedTrainer = decryptedTrainers.FirstOrDefault(d => d.UserId == trainerId);

                if (decryptedTrainer != null) decryptedTrainerName = decryptedTrainer.DecryptedDisplayName;

                if (decryptedUser != null)
                {
                    var countryActivityContract = new CountryActivityContract
                    {
                        TraineeUserName = decryptedUser.DecryptedDisplayName,
                        TrainerUserName = decryptedTrainerName,
                        TraineeActivityLanguageContract = traineeActivityLanguageContracts
                    };
                    countryActivityContracts.Add(countryActivityContract);
                }

            }

            countryActivitiesContract.CountryActivityContract = countryActivityContracts;

            return countryActivitiesContract;

        }
        public async Task<CountryActivitiesContract> GetCountryActivities(int countryId)
        {
            var countryActivitiesContract = new CountryActivitiesContract();
            var countryActivityContracts = new List<CountryActivityContract>();

            var traineeUserIds = await _filterAllowedUser.GetUserIdsFilteredByCountry(countryId);

            if (!traineeUserIds.Any())
            {
                countryActivityContracts.Add(new CountryActivityContract
                {
                    TraineeUserName = "You have no students assigned"
                });
                countryActivitiesContract.CountryActivityContract = countryActivityContracts;

                return countryActivitiesContract;
            }

            var decryptedUsers = _userCommands.GetDecryptedUsers(traineeUserIds);

            var traineeWithTrainerIds = await _filterAllowedUser.GetUserandParentIdsFilteredByCountryId(countryId);

            var trainersIds = await _traininerCommands.GetTrainersIdsByCountryId(countryId);

            var decryptedTrainers = _userCommands.GetDecryptedUsers(trainersIds);

            var userRoles = await _userRoleCommands.GetCultureRolesForUsers(traineeUserIds);

            var groups = await _baseCommands.GetAllAsync<Group>();

            _groups = groups.ToList();
            
            await GetExamsAndAttempts(traineeUserIds);
            await GetCertificatesAchived(traineeUserIds, _groups);
            
            var groupTypes = await _groupTypeCommands.GetAllAvailableGroupTypes();

            foreach (var traineeUserId in traineeUserIds)
            {
                var userId = traineeUserId;

                var userPostsViewedGroupedFirst = _userPostsViewedGrouped.FirstOrDefault(a => a.Key == userId);

                if (userPostsViewedGroupedFirst != null) _userPostsViewedGroupedPerUser = userPostsViewedGroupedFirst.Select(a => a).ToList();

                _allUserAttempts = _allAttempts.FirstOrDefault(p => p.Key == userId);
                _userCertificatesAchieved = _certificatesAchieved.Where(c => c.UserId == userId);

                var userCultures = GetCultureAndLanguageForUser(userId, userRoles);

                var traineeActivityLanguageContracts = new List<TraineeActivityLanguageContract>();


                foreach (var userCulture in userCultures)
                {
                    var groupActivityContracts = new List<GroupActivityContract>();

                    foreach (var groupType in groupTypes)
                    {
                        var groupActivityContract = new GroupActivityContract
                        {
                            Culture = userCulture.Key,
                            GroupTypeId = groupType.ID,
                            ActivityStatus = GetActivitiesStatus(GetGroupIdsForCulture(groupType.ID, userCulture.Key))
                        };
                        groupActivityContracts.Add(groupActivityContract);
                    }

                    var traineeActivityLanguageContract = new TraineeActivityLanguageContract
                    {
                        Language = userCulture.Value,
                        CultureCode = userCulture.Key,
                        GroupActivityContract = groupActivityContracts
                    };

                    traineeActivityLanguageContracts.Add(traineeActivityLanguageContract);
                }

                var decryptedUser = decryptedUsers.FirstOrDefault(d => d.UserId == traineeUserId);
                var trainerId = traineeWithTrainerIds.FirstOrDefault(t => t.Key == traineeUserId).Value;
                var decryptedTrainerName = string.Empty;
                var decryptedTrainer = decryptedTrainers.FirstOrDefault(d => d.UserId == trainerId);

                if (decryptedTrainer != null) decryptedTrainerName = decryptedTrainer.DecryptedDisplayName;

                if (decryptedUser != null)
                {
                    var countryActivityContract = new CountryActivityContract
                    {
                        TraineeUserName = decryptedUser.DecryptedDisplayName,
                        TrainerUserName = decryptedTrainerName,
                        TraineeActivityLanguageContract = traineeActivityLanguageContracts
                    };
                    countryActivityContracts.Add(countryActivityContract);
                }

            }

            countryActivitiesContract.CountryActivityContract = countryActivityContracts;

            return countryActivitiesContract;
        }

        private async Task GetExamsAndAttempts(List<int> traineeUserIds)
        {
            var trainingsExams = await _baseCommands.GetConditionalWithIncludesAsync<TrainingsExam>(x => x.IsLive && x.Exam.StatusId == (int)Status.Live, inc => inc.Exam);
            
            _trainingsExams = trainingsExams.GroupBy(a => a.GroupId, a => a.ExamId).ToList();

            _allAttempts = await _attemptsCommands.GetAttempedExamIdsGroupedByUserIdList(traineeUserIds);;

            _userPostsViewedGrouped = await _userPostViewedCommands.GetUserPostsViewedGroupedList(traineeUserIds);
        }

        private async Task GetCertificatesAchived(List<int> traineeUserIds, List<Group> groups)
        {
            var groupIds = groups.Select(g => g.GroupID);

            _certificatesAchieved =
              await _certificatesAchievedCommands.GetCertificatesAchievedForUsersAndGroups(traineeUserIds, groupIds);
        }

        private Dictionary<string, string> GetCultureAndLanguageForUser(int userId, List<UserRole> userRoles)
        {
            if (userRoles == null || !userRoles.Any()) return null;

            var userCultureRoles = userRoles.Where(c => c.UserID == userId).Select(a => a.askCore_Roles);

            var cultureDictionary = new Dictionary<string, string>();
            
            foreach (var userCultureRole in userCultureRoles)
            {
                var languageName = "Global";
                if (userCultureRole.RoleName != "Maylasian" && userCultureRole.RoleName != "en")
                {
                    languageName = CultureInfo.GetCultureInfo(userCultureRole.RoleName).DisplayName;
                }

                cultureDictionary.Add(userCultureRole.RoleName, languageName);
            }
            
            return cultureDictionary;
        }

        private IEnumerable<int> GetGroupIdsForCulture(int groupTypeId, string culture)
        {
            var groups = _groups.Where (g => g.Culture == culture && g.GroupTypeID == groupTypeId);
            return groups.Select(g => g.GroupID);
        }

        private ActivitiesStatus GetActivitiesStatus(IEnumerable<int> groupIds)
        {
            var trainingsExamsForGroup = _trainingsExams.FirstOrDefault(a => groupIds.Contains(a.Key));

            var isCertified = false;
            if (_userCertificatesAchieved != null)
            {
                isCertified = _userCertificatesAchieved.Any(c => groupIds.Contains(c.GroupId));
            }

            if (trainingsExamsForGroup == null) return ActivitiesStatus.NoExams;

            var totalExams = trainingsExamsForGroup.Select(a => a).ToList();
            
            var userAttempts = false;

            if (_allUserAttempts != null && _allUserAttempts.Any()) userAttempts = _allUserAttempts.Any(totalExams.Contains);

            var userPagesViewed = _userPostsViewedGroupedPerUser.Any(groupId => groupIds.Contains(groupId));

            if (isCertified) return ActivitiesStatus.Certified;

            if (userAttempts) return ActivitiesStatus.SelfAssessmentsInProgress;

            if (userPagesViewed) return ActivitiesStatus.PagesViewed;

            return ActivitiesStatus.NotStarted;
        }
    }

    public class UserIDDTO
    {
        public int UserID { get; set; }
    }
}
