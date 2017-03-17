using System.Collections.Generic;
using LP.EntityModels;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.Dashboards.Regional
{
    public class RegionalDropdownListsViewModel : DropdownListsViewModel
    {
        public DropdownViewModel CountryList { get; set; }
    }
}
