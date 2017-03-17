using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class GroupResponseContract
    {
        public GroupResponseContract()
        {
            Groups = new List<Group>();
        }

        public List<Group> Groups { get; set; }
    }
}
