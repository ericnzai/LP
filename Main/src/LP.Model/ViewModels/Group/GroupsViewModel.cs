using System.Collections.Generic;

namespace LP.Model.ViewModels.Group
{
    public class GroupsViewModel
    {
         public GroupsViewModel()
        {
            Groups = new List<LP.ServiceHost.DataContracts.Common.Content.Group>();
        }

         public List<LP.ServiceHost.DataContracts.Common.Content.Group> Groups { get; set; }
    }
}
