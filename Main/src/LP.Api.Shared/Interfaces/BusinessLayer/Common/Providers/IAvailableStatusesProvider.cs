using System.Collections.Generic;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers
{
    public interface IAvailableStatusesProvider
    {
        Task<IEnumerable<int?>> GetAvailableStatusesForDisplayingModuleInfoAsync(IEnumerable<int> userRoles);
        IEnumerable<int?> GetAvailableStatusesForDashboardPiecharts();
        Task<IEnumerable<int?>> GetAvailableStatusesForOpeningModuleAsync(IEnumerable<int> userRoles);
    }
}
