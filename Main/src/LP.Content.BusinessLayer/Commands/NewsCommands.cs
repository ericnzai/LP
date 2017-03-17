using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Extensions;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class NewsCommands : INewsCommands
    {
        private readonly IBaseCommands _baseCommands;

        public NewsCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<LatestNewsResponseContract> GetLatestNewsAsync(CultureInfo currentRequestCultureInfo)
        {
            var news = await _baseCommands.GetAllAsync<News>();

            var orderedNews = news.Where(s => s.Status == (int)Status.Live).OrderByDescending(d => d.Date).Take(3);

            var latestNewsResponseContract = new LatestNewsResponseContract();

            foreach (var latestNewsItem in orderedNews)
            {
                latestNewsResponseContract.LatestNewsItems.Add(new LatestNewsItem
                {
                    Content = latestNewsItem.BodyText.StripHtmlTags().TruncateAtWord(200),
                    Date = latestNewsItem.Date.ToStringWithCulture(currentRequestCultureInfo),
                    NewsId = latestNewsItem.NewsID
                });
            }

            return latestNewsResponseContract;
        }
    }
}
