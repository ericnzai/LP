using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Filters
{
    public class PostPermissionFilter : IPostPermissionFilter
    {
        private readonly IBaseCommands _baseCommands;

        public PostPermissionFilter(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<IQueryable<int>> AllowedLivePostIds(UserDetails userDetails)
        {
            var posts = await AllowedLivePosts(userDetails);

            return posts.Select(p => p.PostID);
        }

        public async Task<IQueryable<ltl_Posts>> AllowedLivePosts(UserDetails userDetails)
        {
            var currentAllowedGroupPermissions = await
                _baseCommands.GetConditionalWithIncludesAsync<GroupPermission>(
                    x => userDetails.RoleIds.Contains(x.RoleID),
                    grp => grp.ltl_Groups.ltl_Sections.Select(p => p.ltl_Posts));

            var groups = currentAllowedGroupPermissions.Where(a => a.ltl_Groups.StatusBankID == (int) Status.Live);

            var sections = groups.SelectMany(
                a => a.ltl_Groups.ltl_Sections.Where(status => status.Status == (int)Status.Live));

            return sections.SelectMany(p => p.ltl_Posts.Where(status => status.PostStatus == (int)Status.Live));
        }
    }
}
