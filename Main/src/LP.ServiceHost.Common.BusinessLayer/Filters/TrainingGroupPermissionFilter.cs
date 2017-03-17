using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Filters
{
    public class TrainingGroupPermissionFilter : ITrainingGroupPermissionFilter
    {
        private readonly IAvailableStatusesProvider _availableStatusesProvider;

        public TrainingGroupPermissionFilter(IAvailableStatusesProvider availableStatusesProvider)
        {
            _availableStatusesProvider = availableStatusesProvider;
        }

        public IQueryable<Group> GetAvailableByPermissionsOf<T>(IQueryable<Group> groups, IEnumerable<int> userRoleIds)
        {
            if (typeof(T) == typeof(Group))
            {
                return groups.Where(g => g.ltl_GroupPermissions.Any(gp => userRoleIds.Contains(gp.RoleID)));
            }
            if (typeof(T) == typeof(TrainingArea))
            {
                return groups.Where(g => g.TrainingArea.TrainingAreaPermissions.Any(gp => userRoleIds.Contains(gp.RoleID)));
            }

            throw HandleInvalidGenericType(typeof(T));
        }

        public async Task<IQueryable<Group>> GetAvailableByStatusOfAsync<T>(IQueryable<Group> groups, IEnumerable<int> userRoleIds, AccessType accessType)
        {
            if (groups == null)
            {
                return null;
            }

            IEnumerable<int?> availableStatuses;
            switch (accessType)
            {
                case AccessType.DisplayOnDashboards:
                    {
                        availableStatuses = _availableStatusesProvider.GetAvailableStatusesForDashboardPiecharts();
                    }
                    break;
                case AccessType.DisplayOnTraining:
                    {
                        availableStatuses = await _availableStatusesProvider.GetAvailableStatusesForDisplayingModuleInfoAsync(userRoleIds);
                    }
                    break;
                case AccessType.Open:
                    {
                        availableStatuses = await _availableStatusesProvider.GetAvailableStatusesForOpeningModuleAsync(userRoleIds);
                    }
                    break;
                default:
                    {
                        availableStatuses = await _availableStatusesProvider.GetAvailableStatusesForOpeningModuleAsync(userRoleIds);
                    }
                    break;
            }


            if (typeof(T) == typeof(Group))
            {
                return groups.Where(g => availableStatuses.Contains(g.StatusBankID));
            }
            if (typeof(T) == typeof(TrainingArea))
            {
                return groups.Where(g => availableStatuses.Contains(g.TrainingArea.StatusBankID));
            }
            throw HandleInvalidGenericType(typeof(T));
        }

        private static Exception HandleInvalidGenericType(Type providedType)
        {
            return new ArgumentException("Type of entity can be either Group or TrainingArea, provided entity was:" + providedType);
        }
    }
}
