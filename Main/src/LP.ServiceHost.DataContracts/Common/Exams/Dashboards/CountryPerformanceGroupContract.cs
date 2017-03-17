namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class CountryPerformanceGroupContract
    {
        public int GroupTypeId { get; set; }
        public int NumberOfUsersCertified { get; set; }
        public int NumberOfUsersWithAccess { get; set; }
    }
}
