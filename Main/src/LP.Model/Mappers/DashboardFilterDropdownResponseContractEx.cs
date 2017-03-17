using System.Collections.Generic;
using System.Globalization;
using LP.Model.ViewModels.Shared;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Model.Mappers
{
    public static class DashboardFilterDropdownResponseContractEx
    {
        public static List<DropdownItemViewModel> ToViewModels(
            this DashboardFilterDropdownResponseContract dashboardFilterDropdownResponseContract, string defaultText)
        {
            var dropdownItemViewModels = new List<DropdownItemViewModel>();

            foreach (var dropdownItemContract in dashboardFilterDropdownResponseContract.DropdownItemContracts)
            {
                var dropdownItemViewModel = new DropdownItemViewModel
                {
                    DisplayCheckbox = false,
                    Id = dropdownItemContract.Value.ToString(CultureInfo.InvariantCulture),
                    Name = dropdownItemContract.Text
                };

                if (dropdownItemContract.Value == 0 && !string.IsNullOrEmpty(defaultText))
                {
                    dropdownItemViewModel.Name = defaultText;
                }

                dropdownItemViewModels.Add(dropdownItemViewModel);
            }

            return dropdownItemViewModels;
        }
    }
}
