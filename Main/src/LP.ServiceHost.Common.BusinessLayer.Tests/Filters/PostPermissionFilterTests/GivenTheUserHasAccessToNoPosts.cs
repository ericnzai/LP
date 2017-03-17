using System;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Filters.PostPermissionFilterTests
{
    public class GivenTheUserHasAccessToNoPosts : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenACorrectRequestToGetPosts : GivenTheUserHasAccessToNoPosts
        {
            private IQueryable<ltl_Posts> _posts; 

            protected override async void When()
            {
                _posts = await SUT.AllowedLivePosts(UserDetails);
            }

            [Test]
            public void ThenGetAllGroupPermissionsConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<GroupPermission, bool>>>(),
                   It.IsAny<Expression<Func<GroupPermission, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenPostsIsNotNull()
            {
                Assert.IsNotNull(_posts);
            }

            [Test]
            public void ThenNoPostsAreReturned()
            {
                CollectionAssert.IsEmpty(_posts);
            }
        }

        public class WhenACorrectRequestToGetPostIds : GivenTheUserHasAccessToNoPosts
        {
            private IQueryable<int> _postIds;

            protected override async void When()
            {
                _postIds = await SUT.AllowedLivePostIds(UserDetails);
            }

            [Test]
            public void ThenGetAllGroupPermissionsConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<GroupPermission, bool>>>(),
                   It.IsAny<Expression<Func<GroupPermission, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenPostsIsNotNull()
            {
                Assert.IsNotNull(_postIds);
            }

            [Test]
            public void ThenNoPostsAreReturned()
            {
                CollectionAssert.IsEmpty(_postIds);
            }
        }
    }
}
