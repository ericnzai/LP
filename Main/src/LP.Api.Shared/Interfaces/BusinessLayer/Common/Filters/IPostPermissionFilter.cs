using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters
{
    public interface IPostPermissionFilter
    {
        Task<IQueryable<int>> AllowedLivePostIds(UserDetails userDetails);
        Task<IQueryable<ltl_Posts>> AllowedLivePosts(UserDetails userDetails);
    }
}
