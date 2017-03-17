using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels.Exam;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface ICertificatesAchievedCommands
    {
        Task<IEnumerable<CertificateAchievedInformation>> GetCertificatesAchievedByUserForGroups(
            UserDetails userDetails, IEnumerable<int> groupIds);
        Task<IEnumerable<CertificateAchievedInformation>> GetCertificatesAchievedByUser(UserDetails userDetails);
        Task<IQueryable<CertificatesAchieved>> GetCertificatesAchievedForUser(UserDetails userDetails);
        Task<int> GetNumberOfUsersCertififedForGroupType(int groupTypeId);
        Task<int> GetNumberOfUsersCertififedForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds);
        Task<int> GetNumberOfUsersCertififedForGroupTypeByCountry(int groupTypeId, IEnumerable<int> countryUserIds);

        int GetNumberOfUsersCertififedForGroupTypeByCountry(int groupTypeId, 
            IEnumerable<int> queriedGroupIds, IEnumerable<CertificatesAchieved> certificatesAchieved);

        Task<IQueryable<CertificatesAchieved>> GetCertificatesAchievedForCountryUsers(
            IEnumerable<int> countryUserIds);

        int GetNumberOfUsersCertififedForGroupIds(IEnumerable<int> groupIds,
            List<CertificatesAchieved> certificatesAchieved);

        Task<List<CertificatesAchieved>> GetCertificatesAchievedForUsersAndGroups(IEnumerable<int> userIds,
            IEnumerable<int> groupIds);
    }
}
