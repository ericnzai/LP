using System;
using System.Collections.Generic;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolTranslationCommandsTests
{
    public class GivenSavingAConversionToolTranslation : BaseGiven
    {
        public VAConversionToolTranslationDetailsResponseContract _conversionToolTranslationDetailsResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenAConversionToolIsAddedFirstTime : GivenSavingAConversionToolTranslation
        {
            protected override async void When()
            {
                _conversionToolTranslationDetailsResponseContract = await SUT.SaveVAConversionToolTranslation("en", "test2.pdf", "C:/perm/", "C:/temp/", true);
            }

            [Test]
            public void ThenSaveChangesIsCalled()
            {
                const int expected = 1;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenConversionToolResponseIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract);
            }

            [Test]
            public void ThenConversionToolResponseHistoryIsEmpty()
            {
                Assert.IsEmpty(_conversionToolTranslationDetailsResponseContract.VAConversionToolTranslations);
            }
        }

        public class WhenAConversionToolIsAddedAndThereAreSeveralOldOns : GivenSavingAConversionToolTranslation
        {
            protected override async void When()
            {
                VAConversionTools = new List<VAConversionTool>()
                {
                    new VAConversionTool()
                    {
                        DateCreated = DateTime.UtcNow,
                        FileName = "test.pdf",
                        Culture = "en",
                        Status = Status.Live,
                        Comments = "comments",
                        VAConversionToolId = 1,
                        CreatedByUserId = 1,
                        CreatedByUser = new User() {DisplayName = "test"}
                    },
                    new VAConversionTool()
                    {
                        DateCreated = DateTime.UtcNow.AddDays(-1),
                        FileName = "test1.pdf",
                        Culture = "en",
                        Status = Status.Live,
                        Comments = "comments 1",
                        VAConversionToolId = 2,
                        CreatedByUserId = 1,
                        CreatedByUser = new User()
                        {
                            DisplayName = "test"
                        },
                    }
                };
                PrepareSut();

                _conversionToolTranslationDetailsResponseContract = await SUT.SaveVAConversionToolTranslation("en", "test.pdf", "C:/perm/test.pdf", "C:/temp/test.pdf", true);
            }

            [Test]
            public void ThenAddVAConversionToolIsCalledOnlyOnce()
            {
                BaseCommandsMock.Verify(m => m.Add<VAConversionTool>(It.IsAny<VAConversionTool>()), Times.Once);
            }

            [Test]
            public void ThenSaveChangesIsCalled()
            {
                const int expected = 1;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenConversionToolResponseIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract);
            }

            [Test]
            public void ThenConversionToolResponseHistoryIsNotEmpty()
            {
                Assert.IsNotEmpty(_conversionToolTranslationDetailsResponseContract.VAConversionToolTranslations);
            }

            [Test]
            public void ThenConversionToolResponseHistoryHasExactlyExpected()
            {
                const int expected = 2;
                Assert.IsTrue(_conversionToolTranslationDetailsResponseContract.VAConversionToolTranslations.Count.Equals(expected));
            }
        }
    }
}
