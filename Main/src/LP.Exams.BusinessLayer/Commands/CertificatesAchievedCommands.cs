using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.EntityModels;

namespace LP.Exams.BusinessLayer.Commands
{
    public class CertificatesAchievedCommands : ICertificatesAchievedCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IFilterCertificatesAchieved _filterCertificatesAchieved;
        private readonly IFilterAllowedUser _allowedUserFilter;
        private readonly IFilterAllowedGroups _filterAllowedGroups;

        public CertificatesAchievedCommands(IBaseCommands baseCommands, 
            IFilterCertificatesAchieved filterCertificatesAchieved, 
            IFilterAllowedUser allowedUserFilter,
            IFilterAllowedGroups filterAllowedGroups)
        {
            _baseCommands = baseCommands;
            _filterCertificatesAchieved = filterCertificatesAchieved;
            _allowedUserFilter = allowedUserFilter;
            _filterAllowedGroups = filterAllowedGroups;
        }

        public async Task<IEnumerable<CertificateAchievedInformation>> GetCertificatesAchievedByUserForGroups(UserDetails userDetails, IEnumerable<int> groupIds)
        {
            var certificatesAchievedForUser = await GetCertificatesAchievedForUser(userDetails.UserId);

            var certificatesAchievedForUserForGroups =
                certificatesAchievedForUser.Where(g => groupIds.Contains(g.GroupId));

            return certificatesAchievedForUserForGroups.Select(ca => new CertificateAchievedInformation
            {
                AttemptDate = ca.Attempt.AttemptStarted,
                FinishedExam = ca.DateTimeAchieved,
                GroupName = ca.GroupName,
                CertificateDisplayName = ca.Certificate.DisplayName,
                CertificateFileName = ca.Certificate.Filename
            });
        }

        public async Task<IEnumerable<CertificateAchievedInformation>> GetCertificatesAchievedByUser(UserDetails userDetails)
        {
            var certificates = await GetCertificatesAchievedForUser(userDetails.UserId);
            var certificatesAchieved =  await _filterCertificatesAchieved.GetOnlyWithLiveGroup(certificates);

            return certificatesAchieved.Select(ca => new CertificateAchievedInformation
            {
                AttemptDate = ca.Attempt.AttemptStarted,
                FinishedExam = ca.DateTimeAchieved,
                GroupName = ca.GroupName,
                CertificateDisplayName = ca.Certificate.DisplayName,
                CertificateFileName = ca.Certificate.Filename
            });
        }

        private async Task<IQueryable<CertificatesAchieved>> GetCertificatesAchievedForUser(int userId)
        {
            var certificatesAchieved = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            return certificatesAchieved.Where(u => u.UserId == userId);
        }

        public async Task<IQueryable<CertificatesAchieved>> GetCertificatesAchievedForUser(UserDetails userDetails)
        {
            return await GetCertificatesAchievedForUser(userDetails.UserId);
        }

        public async Task<List<CertificatesAchieved>> GetCertificatesAchievedForUsersAndGroups(IEnumerable<int> userIds,
            IEnumerable<int> groupIds)
        {
            var certificatesAchieved =
                await
                    _baseCommands.GetConditionalAsync<CertificatesAchieved>(
                        x => groupIds.Contains(x.GroupId) && userIds.Contains(x.UserId));

            return await certificatesAchieved.ToListAsync();

        }

        public async Task<int> GetNumberOfUsersCertififedForGroupType(int groupTypeId)
        {
            var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            var attempts = await _baseCommands.GetAllAsync<CertificatesAchieved>();
            var userIds = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReportsIds();

            return attempts.Where(g => queriedGroupIds.Contains(g.GroupId) && userIds.Contains(g.UserId))
                .Select(a => a.UserId).Distinct().Count();
        }

        public int GetNumberOfUsersCertififedForGroupIds(IEnumerable<int> groupIds, List<CertificatesAchieved> certificatesAchieved)// List<int> userIds)
        {
            //var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            //var attempts = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            return certificatesAchieved.Where(g => groupIds.Contains(g.GroupId)).Select(a => a.UserId).Distinct().Count();
        }

        public async Task<int> GetNumberOfUsersCertififedForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds)
        {
            var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            var attempts = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            return attempts.Where(g => queriedGroupIds.Contains(g.GroupId) && regionUserIds.Contains(g.UserId))
                .Select(a => a.UserId).Distinct().Count();
        }

        public async Task<int> GetNumberOfUsersCertififedForGroupTypeByCountry(int groupTypeId, IEnumerable<int> countryUserIds)
        {
            var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            var attempts = await _baseCommands.GetAllAsync<CertificatesAchieved>();

            return attempts.Where(g => queriedGroupIds.Contains(g.GroupId) && countryUserIds.Contains(g.UserId))
                .Select(a => a.UserId).Distinct().Count();
        }

        public int GetNumberOfUsersCertififedForGroupTypeByCountry(int groupTypeId, IEnumerable<int> queriedGroupIds,
            IEnumerable<CertificatesAchieved> certificatesAchieved)
        {
            var certificatesAchievedForGroups = certificatesAchieved.Where(g => queriedGroupIds.Contains(g.GroupId));

            var userIds = certificatesAchievedForGroups.Select(u => u.UserId);

            var distinct = userIds.Distinct();

            var count = distinct.Count();

            return count;
        }

        public async Task<IQueryable<CertificatesAchieved>> GetCertificatesAchievedForCountryUsers(IEnumerable<int> countryUserIds)
        {
            return await _baseCommands.GetConditionalAsync<CertificatesAchieved>(g => countryUserIds.Contains(g.UserId));
        }
    }
}
