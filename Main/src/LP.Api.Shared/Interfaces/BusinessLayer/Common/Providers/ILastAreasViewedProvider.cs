using System.Collections.Generic;
using LP.EntityModels;
using LP.EntityModels.StoredProcedure.Output;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers
{
    public interface ILastAreasViewedProvider
    {
        IEnumerable<LastSectionsViewed> GetLastSectionsViewed(int userId, IEnumerable<Group> groups);
    }
}
