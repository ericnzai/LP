using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class LatestNewsResponseContract
    {
        public LatestNewsResponseContract()
        {
            LatestNewsItems = new List<LatestNewsItem>();
        }

        public List<LatestNewsItem> LatestNewsItems { get; set; }  
    }
}
