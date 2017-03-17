using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.UrlMapperCommandsTests
{
    public class GivenMappingAUrlToAPost : BaseGiven
    {
        private string _url;

        protected override void Given()
        {
            Posts = new List<ltl_Posts>
            {
                new ltl_Posts
                {
                    SortOrder = 99,
                    PostID = ExistingPostId,
                    ltl_Sections = new ltl_Sections
                    {
                        FriendlyUrl = "secturl",
                        ltl_Groups = new Group
                        {
                            FriendlyUrl = "groupurl",
                            TrainingArea = new TrainingArea
                            {
                                FriendlyUrl = "trainingurl"
                            }
                        }
                    }
                }
            };

            PrepareSut();
        }

        public class WhenACorrectRequestIsMadeAndThePostExists : GivenMappingAUrlToAPost
        {
            protected override async void When()
            {
                _url = await SUT.MapUrlForPost(ExistingPostId);
            }

            [Test]
            public void ThenGetPostsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(
               m =>
                   m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_Posts, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetFrontEndWebUrlFromConfigurationProviderIsCalledOnce()
            {
                ConfigurationProviderMock.Verify(m => m.FrontEndWebUrl, Times.Once());
            }

            [Test]
            public void ThenUrlIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_url));
            }

            [Test]
            public void ThenUrlIsCorrect()
            {
                const string expected = "https://frontendweb.url/trainingurl/groupurl/secturl/Page99";

                Assert.AreEqual(expected, _url);
            }
        }

        public class WhenACorrectRequestIsMadeAndThePostDoesNotExist : GivenMappingAUrlToAPost
        {
            protected override async void When()
            {
                _url = await SUT.MapUrlForPost(NonExistantPostId);
            }

            [Test]
            public void ThenGetPostsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(
               m =>
                   m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_Posts, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetFrontEndWebUrlFromConfigurationProviderIsNeverCalled()
            {
                ConfigurationProviderMock.Verify(m => m.FrontEndWebUrl, Times.Never());
            }

            [Test]
            public void ThenUrlIsAnEmptyString()
            {
                Assert.AreEqual(string.Empty, _url);
            }
        }
    }
}
