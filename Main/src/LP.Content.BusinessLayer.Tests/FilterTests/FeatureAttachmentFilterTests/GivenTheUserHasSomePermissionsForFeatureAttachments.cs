using System;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.FilterTests.FeatureAttachmentFilterTests
{
    public class GivenTheUserHasSomePermissionsForFeatureAttachments : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenX : GivenTheUserHasSomePermissionsForFeatureAttachments
        {
            private IQueryable<ltl_FeatureAttachment> _featureAttachments; 

            protected override async void When()
            {
                _featureAttachments = await SUT.FilterAllowedFeatureAttachments(UserDetails);
            }

            [Test]
            public void ThenPostPermissionFilterGetAllowedPostIdsIsCalledOnce()
            {
                PostPermissionFilterMock.Verify(m => m.AllowedLivePostIds(It.IsAny<UserDetails>()), Times.Once());
            }

            [Test]
            public void ThenPostPermissionFilterGetAllowedPostIdsIsCalledOnceWithTheCorrectParameters()
            {
                PostPermissionFilterMock.Verify(m => m.AllowedLivePostIds(It.Is<UserDetails>(x => x.UserId == 34)), Times.Once());
            }

            [Test]
            public void ThenGetFeatureAttachmentsConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<ltl_FeatureAttachment, bool>>>(), It.IsAny<Expression<Func<ltl_FeatureAttachment, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenFeatureAttachmentsIsNotNull()
            {
                Assert.IsNotNull(_featureAttachments);
            }
        }
    }
}
