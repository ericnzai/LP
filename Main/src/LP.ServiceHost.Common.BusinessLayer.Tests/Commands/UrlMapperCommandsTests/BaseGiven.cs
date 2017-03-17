using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.EntityModels;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.UrlMapperCommandsTests
{
    public class BaseGiven : SpecsFor<UrlMapperCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IConfigurationProvider> ConfigurationProviderMock = new Mock<IConfigurationProvider>();
        protected const string FrontEndWebUrl = "https://frontendweb.url/";
        protected List<ltl_Posts> Posts = new List<ltl_Posts>{};
        protected const int ExistingPostId = 312;
        protected const int NonExistantPostId = 376;
        protected ltl_FeatureAttachment FeatureAttachment = new ltl_FeatureAttachment();
        protected List<ltl_FeatureAttachmentTranslation> FeatureAttachmentTranslations = new List<ltl_FeatureAttachmentTranslation>(); 
        protected void PrepareSut()
        {
            var postsMoqDbSet = new MoqDbSetProvider<ltl_Posts>().DbSet(Posts);

            BaseCommandsMock.Setup(
                m =>
                    m.GetWithIncludesAsync<ltl_Posts>(It.IsAny<Expression<Func<ltl_Posts, object>>[]>())).ReturnsAsync(postsMoqDbSet.Object);

            BaseCommandsMock.Setup(m => m.GetConditionalAsync(It.IsAny<Expression<Func<ltl_FeatureAttachmentTranslation, bool>>>()))
                .ReturnsAsync(FeatureAttachmentTranslations.AsQueryable());

            ConfigurationProviderMock.Setup(m => m.FrontEndWebUrl).Returns(FrontEndWebUrl);

            SUT = new UrlMapperCommands(BaseCommandsMock.Object, ConfigurationProviderMock.Object);
        }
    }
}
