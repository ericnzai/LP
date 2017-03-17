using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Filters.PostPermissionFilterTests
{
    public class GivenTheUserHasAccessToSomePostsNotAllGroupsAreLive : BaseGiven
    {
        protected override void Given()
        {
            GroupPermissions = new List<GroupPermission>
            {
                new GroupPermission {
                    ltl_Groups = new Group
                {
                    StatusBankID = (int)Status.Live,
                    ltl_Sections = new List<ltl_Sections>
                    {
                        new ltl_Sections
                        {
                            Status = (int)Status.Live,
                            ltl_Posts = new List<ltl_Posts>
                            {
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 1
                                },
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 2
                                },
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 3
                                }
                            }
                        }
                    }
                }, RoleID = 1},
                new GroupPermission { ltl_Groups = new Group
                {
                    StatusBankID = (int)Status.ComingSoon,
                    ltl_Sections = new List<ltl_Sections>
                    {
                        new ltl_Sections
                        {
                            Status = (int)Status.Live,
                            ltl_Posts = new List<ltl_Posts>
                            {
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 4
                                },
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 5
                                }
                            }
                        }
                    }
                }, RoleID = 2},
                new GroupPermission { ltl_Groups = new Group
                {
                    StatusBankID = (int)Status.Live,
                    ltl_Sections = new List<ltl_Sections>
                    {
                        new ltl_Sections
                        {
                            Status = (int)Status.Live,
                            ltl_Posts = new List<ltl_Posts>
                            {
                                new ltl_Posts
                                {
                                   PostStatus = (int)Status.Live,
                                   PostID = 6
                                }
                            }
                        }
                    }
                }, RoleID = 3}
            };

            PrepareSut();
        }

        public class WhenThePostsAreAllLiveAndTheUserIsGettingPostEntities : GivenTheUserHasAccessToSomePostsNotAllGroupsAreLive
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
            public void ThenTheCorrectAmountOfPostsAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _posts.Count());
            }
        }

        public class WhenThePostsAreAllLiveAndTheUserIsGettingPostIds : GivenTheUserHasAccessToSomePostsNotAllGroupsAreLive
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
            public void ThenTheCorrectAmountOfPostsAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _postIds.Count());
            }

            [Test]
            public void ThenTheCorrectPostsAreReturned()
            {
                var expectedPostIds = new List<int> { 1, 2, 3, 6 };

                CollectionAssert.AreEquivalent(expectedPostIds, _postIds);
            }
        }
    }
}
