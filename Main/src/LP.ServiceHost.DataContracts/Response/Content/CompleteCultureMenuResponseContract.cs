using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class CompleteCultureMenuResponseContract
    {
        public CompleteCultureMenuResponseContract()
        {
            AvailableCultures = new Dictionary<string, string>();
        }

        public Dictionary<string, string> AvailableCultures { get; set; }
    }
}
