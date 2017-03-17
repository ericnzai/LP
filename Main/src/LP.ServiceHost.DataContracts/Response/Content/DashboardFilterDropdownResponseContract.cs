using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class DashboardFilterDropdownResponseContract
    {
        public List<DropdownItemContract> DropdownItemContracts { get; set; } 
    }
}
