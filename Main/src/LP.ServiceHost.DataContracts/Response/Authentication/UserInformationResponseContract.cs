namespace LP.ServiceHost.DataContracts.Response.Authentication
{
    public class UserInformationResponseContract
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string FieldOfEmployment { get; set; }
        public string UserCountry { get; set; }
    }
}
