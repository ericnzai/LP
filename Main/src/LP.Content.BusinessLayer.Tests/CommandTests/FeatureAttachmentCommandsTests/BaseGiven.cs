using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Content.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.Dto;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.FeatureAttachmentCommandsTests
{
    public class BaseGiven : SpecsFor<FeatureAttachmentCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IUrlMapperCommands> UrlMapperCommandsMock = new Mock<IUrlMapperCommands>();
        protected Mock<IFeatureAttachmentFilter> FeatureAttachmentFilterMock = new Mock<IFeatureAttachmentFilter>();
        protected readonly Mock<ICommonCalculatorCommands> CommonCalculatorCommandsMock = new Mock<ICommonCalculatorCommands>(); 
        protected readonly Mock<IPostCommands> PostCommandsMock = new Mock<IPostCommands>();
        protected readonly Mock<IGroupCommands> GroupCommandsMock = new Mock<IGroupCommands>();
        protected const string PostUrl = "https://frontendweb.url/path/to/post/url";
        protected string FeatureAttachmentImageUrl = "/path/to/fa/image.jpg";
        protected List<ltl_FeatureAttachment> FeatureAttachments = new List<ltl_FeatureAttachment>();
        protected List<FeatureAttachmentTranslationDto> FeatureAttachmentTranslationDtos = new List<FeatureAttachmentTranslationDto>();
        
        protected int FeatureAttachmentId = 572;
        protected const string CurrentCulture = "de-DE";
        protected UserDetails UserDetails = new UserDetails{CurrentCulture = CurrentCulture, UserId = 141};
        protected const int CsPostId = 138;
        protected List<ltl_Posts> Posts = new List<ltl_Posts>();
        protected const int ExistingPostId = 487;
        protected int NumberOfItemsToSkip = 0;
 
        protected void PrepareSut()
        {
            UrlMapperCommandsMock.Setup(
                m => m.MapUrlForFeatureAttachmentImage(It.IsAny<ltl_FeatureAttachment>(), It.IsAny<string>()))
                .ReturnsAsync(FeatureAttachmentImageUrl);

            UrlMapperCommandsMock.Setup(
                m => m.MapUrlForPost(It.IsAny<ltl_Posts>()))
                .Returns(PostUrl);

            var featureAttachmentsMoqDbSet = new MoqDbSetProvider<ltl_FeatureAttachment>().DbSet(FeatureAttachments);
           
            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_FeatureAttachment, object>>[]>()))
                .ReturnsAsync(featureAttachmentsMoqDbSet.Object);

            var postsMoqDbSet = new MoqDbSetProvider<ltl_Posts>().DbSet(Posts);

            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_Posts, object>>[]>()))
                .ReturnsAsync(postsMoqDbSet.Object);

            CommonCalculatorCommandsMock.Setup(m => m.GetPagingNumberToSkip(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(NumberOfItemsToSkip);

            FeatureAttachmentFilterMock.Setup(m => m.FilterAllowedFeatureAttachmentTranslations(It.IsAny<UserDetails>()))
                .ReturnsAsync(FeatureAttachmentTranslationDtos.AsEnumerable());

            SUT = new FeatureAttachmentCommands(BaseCommandsMock.Object, UrlMapperCommandsMock.Object, FeatureAttachmentFilterMock.Object, CommonCalculatorCommandsMock.Object, PostCommandsMock.Object, GroupCommandsMock.Object);
        }
    }
}
