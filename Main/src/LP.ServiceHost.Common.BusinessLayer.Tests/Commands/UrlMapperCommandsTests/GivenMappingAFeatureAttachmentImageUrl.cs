using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.UrlMapperCommandsTests
{
    public class GivenMappingAFeatureAttachmentImageUrl : BaseGiven
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

            FeatureAttachmentTranslations = new List<ltl_FeatureAttachmentTranslation>
            {
                new ltl_FeatureAttachmentTranslation
                {
                    FeatureAttachmentID = FeatureAttachment.FeatureAttachmentID,
                    Culture = "en",
                    FileName = "fnameenglish.jpg"
                },
                 new ltl_FeatureAttachmentTranslation
                {
                    FeatureAttachmentID = FeatureAttachment.FeatureAttachmentID,
                    Culture = "tr",
                    FileName = "fnameturkish.jpg"
                }
            };

            PrepareSut();
        }

        public class WhenThePostLinkedToTheFeatureAttachmentExistsAndTheCultureIsGlobalEnglish : GivenMappingAFeatureAttachmentImageUrl
        {
            protected override async void When()
            {
                FeatureAttachment = new ltl_FeatureAttachment
                {
                    CSPostID = ExistingPostId
                };

                PrepareSut();

                _url = await SUT.MapUrlForFeatureAttachmentImage(FeatureAttachment, "en");
            }

            [Test]
            public void ThenGetPostsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m =>
                   m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_Posts, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetFrontEndWebUrlFromConfigurationProviderIsCalledOnce()
            {
                ConfigurationProviderMock.Verify(m => m.FrontEndWebUrl, Times.Once());
            }

            [Test]
            public void ThenGetConditionalAsyncFeatureAttachmentTranslationsIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync(It.IsAny<Expression<Func<ltl_FeatureAttachmentTranslation, bool>>>()), Times.Once());
            }

            [Test]
            public void ThenUrlIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_url));
            }

            [Test]
            public void ThenFeatureAttachmentUrlIsCorrect()
            {
                const string expected = "https://frontendweb.url/Content/groupurl/FeatureAttachments/Images/fnameenglish.jpg";

                Assert.AreEqual(expected, _url);
            }
        }

        public class WhenThePostLinkedToTheFeatureAttachmentExistsAndTheCultureIsTurkish : GivenMappingAFeatureAttachmentImageUrl
        {
            protected override async void When()
            {
                FeatureAttachment = new ltl_FeatureAttachment
                {
                    CSPostID = ExistingPostId
                };

                PrepareSut();

                _url = await SUT.MapUrlForFeatureAttachmentImage(FeatureAttachment, "tr");
            }

            [Test]
            public void ThenGetPostsWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m =>
                   m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_Posts, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetFrontEndWebUrlFromConfigurationProviderIsCalledOnce()
            {
                ConfigurationProviderMock.Verify(m => m.FrontEndWebUrl, Times.Once());
            }

            [Test]
            public void ThenGetConditionalAsyncFeatureAttachmentTranslationsIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync(It.IsAny<Expression<Func<ltl_FeatureAttachmentTranslation, bool>>>()), Times.Once());
            }

            [Test]
            public void ThenUrlIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_url));
            }

            [Test]
            public void ThenFeatureAttachmentUrlIsCorrect()
            {
                const string expected = "https://frontendweb.url/Content/groupurl/FeatureAttachments/Images/fnameturkish.jpg";

                Assert.AreEqual(expected, _url);
            }
        }
    }
}
