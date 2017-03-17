using System.Collections.Generic;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Providers
{
    public class AvailableStatusesProvider : IAvailableStatusesProvider
    {
        private readonly List<int?> _availableStatusesForStandardUserForDisplayingModuleInfo;
        private readonly List<int?> _availableStatusesForStandardUserForOpeningModule;
        private readonly List<string> _roleNamesWithAccessToTranslationInProgress, _roleNamesWithAccessToComingSoon;
        private readonly List<int?> _availableStatusesForDashboardPieCharts;
        private readonly IRoleProvider _roleProvider;
        
        public AvailableStatusesProvider(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;

            _availableStatusesForStandardUserForDisplayingModuleInfo = new List<int?>
            {
                (int)Status.ComingSoon,
                (int)Status.Live
            };
            _availableStatusesForStandardUserForOpeningModule = new List<int?>
            {
                (int)Status.Live
            };
            _availableStatusesForDashboardPieCharts = new List<int?>
            {
                (int)Status.Live,
                (int)Status.ComingSoon
            };

            _roleNamesWithAccessToTranslationInProgress = new List<string>
            {
                "Administrator", "Admin_ContentTranslationManagement"
            };
            _roleNamesWithAccessToComingSoon = new List<string>
            {
                "Administrator"
            };
        }

        public IEnumerable<int?> GetAvailableStatusesForDashboardPiecharts()
        {
            return _availableStatusesForDashboardPieCharts;
        }

        public async Task<IEnumerable<int?>> GetAvailableStatusesForDisplayingModuleInfoAsync(IEnumerable<int> userRolesIds)
        {
            var statuses = _availableStatusesForStandardUserForDisplayingModuleInfo;
            statuses.AddRange(( await GetAdditionalStatusesBasedOnRoleForDisplayingModuleInfoAsync(userRolesIds)));
            return statuses;
        }

        public async Task<IEnumerable<int?>> GetAvailableStatusesForOpeningModuleAsync(IEnumerable<int> userRolesIds)
        {
            var statuses = _availableStatusesForStandardUserForOpeningModule;
            statuses.AddRange(( await GetAdditionalStatusesBasedOnRoleForOpeningModuleAsync(userRolesIds)));
            return statuses;
        }

        private async Task<IEnumerable<int?>> GetAdditionalStatusesBasedOnRoleForDisplayingModuleInfoAsync(IEnumerable<int> userRolesIds)
        {
            var additionalStatuses = new List<int?>();
            
            if (await _roleProvider.IsUserInAnyGivenRoleAsync(_roleNamesWithAccessToTranslationInProgress, userRolesIds))
            {
                additionalStatuses.Add((int)Status.TranslationInProgress);
            }

            return additionalStatuses;
        }

        private async Task<IEnumerable<int?>> GetAdditionalStatusesBasedOnRoleForOpeningModuleAsync(IEnumerable<int> userRolesIds)
        {

            var additionalStatuses = new List<int?>();
            if (await _roleProvider.IsUserInAnyGivenRoleAsync(_roleNamesWithAccessToTranslationInProgress, userRolesIds))
            {
                additionalStatuses.Add((int)Status.TranslationInProgress);
            }
            if (await _roleProvider.IsUserInAnyGivenRoleAsync(_roleNamesWithAccessToComingSoon, userRolesIds))
            {
                additionalStatuses.Add((int)Status.ComingSoon);
            }
            return additionalStatuses;
        }
    }
}
