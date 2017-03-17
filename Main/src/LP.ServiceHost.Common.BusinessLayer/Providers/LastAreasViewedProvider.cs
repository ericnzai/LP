using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.StoredProcedure.Input;
using LP.EntityModels.StoredProcedure.Output;

namespace LP.ServiceHost.Common.BusinessLayer.Providers
{
    public class LastAreasViewedProvider : ILastAreasViewedProvider
    {
        private readonly IBaseCommands _baseCommands;

        public LastAreasViewedProvider(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public IEnumerable<LastSectionsViewed> GetLastSectionsViewed(int userId, IEnumerable<Group> groups)
        {
            var groupsList = groups.Select(g => g.GroupID.ToString(CultureInfo.InvariantCulture)).Distinct().Aggregate((g1, g2) => string.Format("{0},{1}", g1, g2));

            if (string.IsNullOrEmpty(groupsList)) return new List<LastSectionsViewed>();

            var args = new SpGetLastViewedSectionsArguments { UserId = userId, GroupIdListAsString = groupsList };

            var lastSectionsViewed = _baseCommands.ExecuteStoredProcedure<LastSectionsViewed, SpGetLastViewedSectionsArguments>(args).ToList();

            return lastSectionsViewed.OrderByDescending(g => g.DateTime);
        }
    }
}
