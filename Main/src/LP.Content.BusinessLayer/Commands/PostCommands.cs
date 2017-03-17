using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class PostCommands: IPostCommands
    {

        private readonly IBaseCommands _baseCommands;

        public PostCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<PostResponseContract> GetGroupNameByPostId(int postId)
        {
            //using (var context = DBContextHelper.DataContext)
            //{
            //    return context.Posts().Queryable()
            //        .Where(p => p.PostID == postId && p.Section != null && p.Section.Groups != null)
            //        .Select(p => p.Section.Groups.Name)
            //        .SingleOrDefault();
            //}

            //var x = await _baseCommands.

            var posts = await _baseCommands.GetConditionalWithIncludesAsync<EntityModels.ltl_Posts>(p => p.PostID == postId && p.ltl_Sections != null && p.ltl_Sections.ltl_Groups != null, inc=>inc.ltl_Sections);

            //var groupId = posts.Where(p => p.PostID == postId && p.ltl_Sections != null && p.ltl_Sections.ltl_Groups != null)

            var response = new PostResponseContract()
            {
                GroupName = posts.Select(p => p.ltl_Sections.ltl_Groups.Name).SingleOrDefault()
            };
            return response;
        }
    }
}
