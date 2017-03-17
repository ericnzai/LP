using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.FeatureAttachmentCommandsTests
{
    public class GivenGettingVideoModalFeatureAttachments : BaseGiven
    {
        private FeatureAttachmentVideoModalResponseContract _featureAttachmentVideoModalResponseContract;

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

        public class WhenARequestIsMadeForAnExistingFeatureAttachment : GivenGettingVideoModalFeatureAttachments
        {
            private const string Title = "ThisIsTheTitle";
            private const string Body = "ThisIsTheBody";
            protected override async void When()
            {
                FeatureAttachments = new List<ltl_FeatureAttachment>
                {
                    new ltl_FeatureAttachment
                    {
                        CSPostID = ExistingPostId,
                        FeatureAttachmentID = FeatureAttachmentId,
                        Title = Title,
                        Body = Body,
                        ltl_FeatureAttachmentTranslation = new List<ltl_FeatureAttachmentTranslation>
                        {
                            new ltl_FeatureAttachmentTranslation{Culture = "tr", Title = "turkish title", Body = "turkish body"},
                            new ltl_FeatureAttachmentTranslation{Culture = "en", Title = "global english title", Body = "global english body"} 
                        }
                    }
                };

                PrepareSut();

                _featureAttachmentVideoModalResponseContract = await SUT.GetFeatureAttachmentVideoModalResponseContract(FeatureAttachmentId, UserDetails);
            }

            [Test]
            public void ThenGetFeatureAttachmentByIdAsyncIsNeverCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_FeatureAttachment>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGetFeatureAttachmentWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_FeatureAttachment, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenMapUrlForFeatureAttachmentImageIsNeverCalledOnce()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForFeatureAttachmentImage(It.IsAny<ltl_FeatureAttachment>(), It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenMapUrlForPostIsCalledOnce()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForPost(It.IsAny<ltl_Posts>()), Times.Once());
            }

            [Test]
            public void ThenMapUrlForPostIsCalledOnceWithTheCorrectParameters()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForPost(It.Is<ltl_Posts>(x => x.PostID == ExistingPostId)), Times.Once());
            }

            [Test]
            public void ThenFeatureAttachmentModalResponseContractIsNotNull()
            {
                Assert.IsNotNull(_featureAttachmentVideoModalResponseContract);
            }

            [Test]
            public void ThenFeatureAttachmentIdIsReturnedCorrectly()
            {
                Assert.AreEqual(FeatureAttachmentId, _featureAttachmentVideoModalResponseContract.FeatureAttachmentId);
            }

            [Test]
            public void ThenPostUrlIsReturnedCorrectly()
            {
                Assert.AreEqual(PostUrl, _featureAttachmentVideoModalResponseContract.FeatureAttachmentPostInformation.PostUrl);
            }

            [Test]
            public void ThenTitleIsReturnedCorrectly()
            {
                const string expected = "global english title";

                Assert.AreEqual(expected, _featureAttachmentVideoModalResponseContract.Title);
            }

            [Test]
            public void ThenDescriptionIsReturnedCorrectly()
            {
                const string expected = "global english body";

                Assert.AreEqual(expected, _featureAttachmentVideoModalResponseContract.Description);
            }
        }

        public class WhenARequestIsMadeForAnExistingFeatureAttachmentThatHasANullCsPostId : GivenGettingVideoModalFeatureAttachments
        {
            private const string Title = "ThisIsTheTitle";
            private const string Body = "ThisIsTheBody";
            protected override async void When()
            {
                FeatureAttachments = new List<ltl_FeatureAttachment>
                {
                    new ltl_FeatureAttachment
                    {
                        CSPostID = null,
                        FeatureAttachmentID = FeatureAttachmentId,
                        Title = Title,
                        Body = Body,
                        ltl_FeatureAttachmentTranslation = new List<ltl_FeatureAttachmentTranslation>
                        {
                            new ltl_FeatureAttachmentTranslation{Culture = "tr", Title = "turkish title", Body = "turkish body"},
                            new ltl_FeatureAttachmentTranslation{Culture = "en", Title = "global english title", Body = "global english body"} 
                        }
                    }
                };

                PrepareSut();

                _featureAttachmentVideoModalResponseContract = await SUT.GetFeatureAttachmentVideoModalResponseContract(FeatureAttachmentId, UserDetails);
            }

            [Test]
            public void ThenGetFeatureAttachmentByIdAsyncIsNeverCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_FeatureAttachment>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGetFeatureAttachmentWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_FeatureAttachment, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenMapUrlForFeatureAttachmentImageIsNeverCalled()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForFeatureAttachmentImage(It.IsAny<ltl_FeatureAttachment>(), It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenMapUrlForPostIsNeverCalled()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForPost(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenFeatureAttachmentModalResponseContractIsNotNull()
            {
                Assert.IsNotNull(_featureAttachmentVideoModalResponseContract);
            }

            [Test]
            public void ThenFeatureAttachmentIdIsReturnedCorrectly()
            {
                Assert.AreEqual(FeatureAttachmentId, _featureAttachmentVideoModalResponseContract.FeatureAttachmentId);
            }

            [Test]
            public void ThenPostUrlIsReturnedCorrectly()
            {
                Assert.IsNull(_featureAttachmentVideoModalResponseContract.FeatureAttachmentPostInformation.PostUrl);
            }

            [Test]
            public void ThenTitleIsReturnedCorrectly()
            {
                const string expected = "global english title";

                Assert.AreEqual(expected, _featureAttachmentVideoModalResponseContract.Title);
            }

            [Test]
            public void ThenDescriptionIsReturnedCorrectly()
            {
                const string expected = "global english body";

                Assert.AreEqual(expected, _featureAttachmentVideoModalResponseContract.Description);
            }
        }
    }
}
