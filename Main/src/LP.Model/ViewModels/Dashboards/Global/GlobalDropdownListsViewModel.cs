using System.Collections.Generic;
using LP.EntityModels;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.Dashboards.Global
{
    public class GlobalDropdownListsViewModel : DropdownListsViewModel
    {
        public DropdownViewModel RegionList { get; set; }
        public DropdownViewModel CountryList { get; set; }
    }
}
