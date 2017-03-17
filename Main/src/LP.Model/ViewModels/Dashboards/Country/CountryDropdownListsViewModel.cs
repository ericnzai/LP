using System.Collections.Generic;
using LP.EntityModels;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Model.ViewModels.Dashboards.Country
{
    public class CountryDropdownListsViewModel :DropdownListsViewModel
    {
        public DropdownViewModel TrainerList { get; set; }
    }
}
