using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface IPostCommands
    {
        Task<PostResponseContract> GetGroupNameByPostId(int postId);
    }
}
