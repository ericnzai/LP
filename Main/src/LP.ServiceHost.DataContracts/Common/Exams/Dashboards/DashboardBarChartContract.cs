namespace LP.ServiceHost.DataContracts.Common.Exams.Dashboards
{
    public class DashboardBarChartContract
    {
        public string Title { get; set; }
        public int NumberOfUsersStarted { get; set; }
        public int PercentageOfUsersStarted { get; set; }
        public int NumberOfUsersInProgress { get; set; }
        public int PercentageOfUsersInProgress { get; set; }
        public int NumberOfUsersCertified { get; set; }
        public int PercentageOfUsersCertified { get; set; }
    }
}
