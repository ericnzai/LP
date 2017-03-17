using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class GlossaryItemsResponseContract
    {
        public GlossaryItemsResponseContract()
        {
            GlossaryItems = new List<GlossaryItem>();
        }
        public List<GlossaryItem> GlossaryItems { get; set; }
    }
}
