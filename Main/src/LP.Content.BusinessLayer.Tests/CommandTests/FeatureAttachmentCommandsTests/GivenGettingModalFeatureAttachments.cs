using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.FeatureAttachmentCommandsTests
{
    public class GivenGettingModalFeatureAttachments : BaseGiven
    {
        private FeatureAttachmentModalResponseContract _featureAttachmentModalResponseContract;

        protected override void Given()
        {
            Posts = new List<ltl_Posts>
            {
                new ltl_Posts
                {
                    SortOrder = 99,
                    PostID = ExistingPostId,
                    ltl_PostTranslations = new Collection<ltl_PostTranslations>
                    {
                        new ltl_PostTranslations {PostName = "Global post name", Culture = "en"},
                        new ltl_PostTranslations {PostName = "Turkish post name", Culture = "tr"}
                    },
                    ltl_Sections = new ltl_Sections
                    {
                        FriendlyUrl = "secturl",
                        ltl_Groups = new Group
                        {
                            FriendlyUrl = "groupurl",
                            TrainingArea = new TrainingArea
                            {
                                FriendlyUrl = "trainingurl"
                            },
                            Name = "group name"
                        },
                        ltl_SectionTranslations = new Collection<ltl_SectionTranslations>
                        {
                            new ltl_SectionTranslations {Culture = "tr", Name = "Turkish section name"}, 
                            new ltl_SectionTranslations {Culture = "en", Name = "Global section name"}
                        }
                    }
                }
            };

            PrepareSut();
        }

        public class WhenARequestIsMadeForAnExistingFeatureAttachment : GivenGettingModalFeatureAttachments
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

                _featureAttachmentModalResponseContract = await SUT.GetFeatureAttachmentModalResponseContract(FeatureAttachmentId, UserDetails);
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
            public void ThenMapUrlForFeatureAttachmentImageIsCalledOnce()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForFeatureAttachmentImage(It.IsAny<ltl_FeatureAttachment>(), It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenMapUrlForFeatureAttachmentImageIsCalledOnceWithTheCorrectParameters()
            {
                UrlMapperCommandsMock.Verify(m => m.MapUrlForFeatureAttachmentImage(It.Is<ltl_FeatureAttachment>(x => x == FeatureAttachments.First()), It.Is<string>(x => x == CurrentCulture)), Times.Once());
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
                Assert.IsNotNull(_featureAttachmentModalResponseContract);
            }

            [Test]
            public void ThenFeatureAttachmentIdIsReturnedCorrectly()
            {
                Assert.AreEqual(FeatureAttachmentId, _featureAttachmentModalResponseContract.FeatureAttachmentId);
            }

            [Test]
            public void ThenPostUrlIsReturnedCorrectly()
            {
                Assert.AreEqual(PostUrl, _featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.PostUrl);
            }

            [Test]
            public void ThenImageUrlIsReturnedCorrectly()
            {
                Assert.AreEqual(FeatureAttachmentImageUrl, _featureAttachmentModalResponseContract.ImageUrl);
            }

            [Test]
            public void ThenTitleIsReturnedCorrectly()
            {
                const string expected = "global english title";

                Assert.AreEqual(expected, _featureAttachmentModalResponseContract.Title);
            }

            [Test]
            public void ThenDescriptionIsReturnedCorrectly()
            {
                const string expected = "global english body";

                Assert.AreEqual(expected, _featureAttachmentModalResponseContract.Description);
            }
        }

        public class WhenARequestIsMadeForAnExistingFeatureAttachmentAndThereIsNoTranslationForTheSelectedCulture : GivenGettingModalFeatureAttachments
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

                UserDetails.CurrentCulture = "de-DE";

                _featureAttachmentModalResponseContract = await SUT.GetFeatureAttachmentModalResponseContract(FeatureAttachmentId, UserDetails);
            }

            [Test]
            public void ThenGlobalEnglishTitleIsReturned()
            {
                Assert.AreEqual("global english title", _featureAttachmentModalResponseContract.Title);
            }

            [Test]
            public void ThenGlobalEnglishDescriptionIsReturned()
            {
                Assert.AreEqual("global english body", _featureAttachmentModalResponseContract.Description);
            }
        }

        public class WhenARequestIsMadeForAnExistingFeatureAttachmentAndThereIsATranslationForTheSelectedCulture : GivenGettingModalFeatureAttachments
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

                UserDetails.CurrentCulture = "tr";

                _featureAttachmentModalResponseContract = await SUT.GetFeatureAttachmentModalResponseContract(FeatureAttachmentId, UserDetails);
            }

            [Test]
            public void ThenTurkishTitleIsReturned()
            {
                Assert.AreEqual("turkish title", _featureAttachmentModalResponseContract.Title);
            }

            [Test]
            public void ThenTurkishDescriptionIsReturned()
            {
                Assert.AreEqual("turkish body", _featureAttachmentModalResponseContract.Description);
            }
        }

        public class WhenARequestIsMadeForAnExistingFeatureAttachmentThatHasANullCsPostId : GivenGettingModalFeatureAttachments
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

                _featureAttachmentModalResponseContract = await SUT.GetFeatureAttachmentModalResponseContract(FeatureAttachmentId, UserDetails);
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
                Assert.IsNotNull(_featureAttachmentModalResponseContract);
            }

            [Test]
            public void ThenFeatureAttachmentIdIsReturnedCorrectly()
            {
                Assert.AreEqual(FeatureAttachmentId, _featureAttachmentModalResponseContract.FeatureAttachmentId);
            }

            [Test]
            public void ThenPostUrlIsReturnedCorrectly()
            {
                Assert.IsNullOrEmpty(_featureAttachmentModalResponseContract.FeatureAttachmentPostInformation.PostUrl);
            }

            [Test]
            public void ThenImageUrlIsReturnedCorrectly()
            {
                Assert.AreEqual(string.Empty, _featureAttachmentModalResponseContract.ImageUrl);
            }

            [Test]
            public void ThenTitleIsReturnedCorrectly()
            {
                const string expected = "global english title";

                Assert.AreEqual(expected, _featureAttachmentModalResponseContract.Title);
            }

            [Test]
            public void ThenDescriptionIsReturnedCorrectly()
            {
                const string expected = "global english body";

                Assert.AreEqual(expected, _featureAttachmentModalResponseContract.Description);
            }
        }
    }
}
