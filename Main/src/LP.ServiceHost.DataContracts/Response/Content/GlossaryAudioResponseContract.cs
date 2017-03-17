namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class GlossaryAudioResponseContract
    {
        public string AudioBase64 { get; set; }
        public string FileName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
