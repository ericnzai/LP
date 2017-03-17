namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class TopicDeleteResponseContract
    {
        public TopicDeleteResponseContract()
        {
            Result = false;
        }
        public bool Result { get; set; }
    }
}
