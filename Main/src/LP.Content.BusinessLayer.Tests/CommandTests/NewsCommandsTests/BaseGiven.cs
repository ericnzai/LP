using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.NewsCommandsTests
{
    public class BaseGiven : SpecsFor<NewsCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();

        protected List<News> News = new List<News>();

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<News>()).ReturnsAsync(News.AsQueryable());

            SUT = new NewsCommands(BaseCommandsMock.Object);
        }
    }
}
