namespace LP.Model.Authentication
{
    public class DecryptedUser
    {
        public int UserId { get; set; }
        public string DecryptedUserName { get; set; }
        public string DecryptedDisplayName { get; set; }
    }
}
