using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.Dto;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.FeatureAttachmentCommandsTests
{
    public class GivenGettingAFeatureAttachmentPage : BaseGiven
    {
        protected override void Given()
        {
            FeatureAttachmentTranslationDtos = new List<FeatureAttachmentTranslationDto>
            {
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 1, SortOrder = 26, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 1, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 2, SortOrder = 25, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 2, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 3, SortOrder = 24, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 3, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 4, SortOrder = 23, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 4, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 5, SortOrder = 22, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 5, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 6, SortOrder = 21, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 6, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 7, SortOrder = 20, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 7, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 8, SortOrder = 19, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 8, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 9, SortOrder = 18, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 9, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 10, SortOrder = 17, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 10, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 11, SortOrder = 16, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 11, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 12, SortOrder = 15, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 12, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 13, SortOrder = 14, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 13, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 14, SortOrder = 13, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 14, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 15, SortOrder = 12, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 15, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 16, SortOrder = 11, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 16, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 17, SortOrder = 10, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 17, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 18, SortOrder = 9, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 18, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 19, SortOrder = 8, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 19, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 20, SortOrder = 7, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 20, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 21, SortOrder = 6, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 21, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 22, SortOrder = 5, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 22, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 23, SortOrder = 4, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 23, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 24, SortOrder = 3, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 24, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 25, SortOrder = 2, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 25, FeatureAttachmentTypeID = 1}},
                new FeatureAttachmentTranslationDto {FeatureAttachmentID = 26, SortOrder = 1, FeatureAttachment = new FeatureAttachmentDto{FeatureAttachmentID = 26, FeatureAttachmentTypeID = 1}}
            };

            PrepareSut();
        }

        public class WhenThereAreEnoughFeatureAttachmentsToPopulateThePageAndTheFirstPageIsBeingReturned : GivenGettingAFeatureAttachmentPage
        {
            private FeatureAttachmentPageResponseContract _featureAttachmentPageResponseContract;
            private const int PageNumber = 1;
            protected override async void When()
            {
                _featureAttachmentPageResponseContract = await SUT.GetFeatureAttachmentPageAsync(PageNumber, UserDetails);
            }

            [Test]
            public void ThenFilterAllowedFeatureAttachmentsIsCalledOnce()
            {
                FeatureAttachmentFilterMock.Verify(m => m.FilterAllowedFeatureAttachmentTranslations(It.IsAny<UserDetails>()), Times.Once());
            }

            [Test]
            public void ThenFilterAllowedFeatureAttachmentsIsCalledOnceWithTheCorrectParameters()
            {
                FeatureAttachmentFilterMock.Verify(m => m.FilterAllowedFeatureAttachmentTranslations(It.Is<UserDetails>(x => x.UserId == 141)), Times.Once());
            }

            [Test]
            public void ThenCommonCalculatorCommandsGetPagingNumberToSkipIsCalledOnce()
            {
                CommonCalculatorCommandsMock.Verify(m => m.GetPagingNumberToSkip(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenCommonCalculatorCommandsGetPagingNumberToSkipIsCalledOnceWithTheCorrectParameters()
            {
                CommonCalculatorCommandsMock.Verify(m => m.GetPagingNumberToSkip(It.Is<int>(x => x == PageNumber), It.Is<int>(x => x == 20)), Times.Once());
            }

            [Test]
            public void ThenFeatureAttachmentPageResponseContractIsNotNull()
            {
                Assert.IsNotNull(_featureAttachmentPageResponseContract);
            }

            [Test]
            public void ThenPageNumberIsReturnedCorrectly()
            {
                Assert.AreEqual(PageNumber, _featureAttachmentPageResponseContract.PageNumber);
            }

            [Test]
            public void ThenFeatureAttachmentsReturnedAreNotNull()
            {
                Assert.IsNotNull(_featureAttachmentPageResponseContract.FeatureAttachmentItems);
            }

            [Test]
            public void ThenTheCorrectAmountOfFeatureAttachmentsAreReturned()
            {
                const int expected = 20;

                Assert.AreEqual(expected, _featureAttachmentPageResponseContract.FeatureAttachmentItems.Count);
            }

            [Test]
            public void ThenNoItemsInTheFeatureAttachmentsReturnedAreNull()
            {
                CollectionAssert.AllItemsAreNotNull(_featureAttachmentPageResponseContract.FeatureAttachmentItems);
            }

            [Test]
            public void ThenTheCorrectFeatureAttachmentIdsAreReturned()
            {
                var expected = new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 };

                CollectionAssert.AreEquivalent(expected, _featureAttachmentPageResponseContract.FeatureAttachmentItems.Select(a => a.FeatureAttachmentId));
            }
        }

        public class WhenThereAreNotEnoughFeatureAttachmentsToPopulateThePageAndTheSecondPageIsBeingReturned : GivenGettingAFeatureAttachmentPage
        {
            private FeatureAttachmentPageResponseContract _featureAttachmentPageResponseContract;
            private const int PageNumber = 2;

            protected override async void When()
            {
                NumberOfItemsToSkip = 20;
                
                PrepareSut();

                _featureAttachmentPageResponseContract = await SUT.GetFeatureAttachmentPageAsync(PageNumber, UserDetails);
            }

            [Test]
            public void ThenFilterAllowedFeatureAttachmentsIsCalledOnce()
            {
                FeatureAttachmentFilterMock.Verify(m => m.FilterAllowedFeatureAttachmentTranslations(It.IsAny<UserDetails>()), Times.Once());
            }

            [Test]
            public void ThenFilterAllowedFeatureAttachmentsIsCalledOnceWithTheCorrectParameters()
            {
                FeatureAttachmentFilterMock.Verify(m => m.FilterAllowedFeatureAttachmentTranslations(It.Is<UserDetails>(x => x.UserId == 141)), Times.Once());
            }

            [Test]
            public void ThenCommonCalculatorCommandsGetPagingNumberToSkipIsCalledOnce()
            {
                CommonCalculatorCommandsMock.Verify(m => m.GetPagingNumberToSkip(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenCommonCalculatorCommandsGetPagingNumberToSkipIsCalledOnceWithTheCorrectParameters()
            {
                CommonCalculatorCommandsMock.Verify(m => m.GetPagingNumberToSkip(It.Is<int>(x => x == PageNumber), It.Is<int>(x => x == 20)), Times.Once());
            }


            [Test]
            public void ThenFeatureAttachmentPageResponseContractIsNotNull()
            {
                Assert.IsNotNull(_featureAttachmentPageResponseContract);
            }

            [Test]
            public void ThenPageNumberIsReturnedCorrectly()
            {
                Assert.AreEqual(PageNumber, _featureAttachmentPageResponseContract.PageNumber);
            }

            [Test]
            public void ThenFeatureAttachmentsReturnedAreNotNull()
            {
                Assert.IsNotNull(_featureAttachmentPageResponseContract.FeatureAttachmentItems);
            }

            [Test]
            public void ThenTheCorrectAmountOfFeatureAttachmentsAreReturned()
            {
                const int expected = 6;

                Assert.AreEqual(expected, _featureAttachmentPageResponseContract.FeatureAttachmentItems.Count);
            }

            [Test]
            public void ThenNoItemsInTheFeatureAttachmentsReturnedAreNull()
            {
                CollectionAssert.AllItemsAreNotNull(_featureAttachmentPageResponseContract.FeatureAttachmentItems);
            }

            [Test]
            public void ThenTheCorrectFeatureAttachmentIdsAreReturned()
            {
                var expected = new List<int> {21, 22, 23, 24, 25, 26};

                CollectionAssert.AreEquivalent(expected, _featureAttachmentPageResponseContract.FeatureAttachmentItems.Select(a => a.FeatureAttachmentId));
            }
        }
    }
}
