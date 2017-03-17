using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolCommandsTests;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolTranslationCommandsTests
{
    public class GivenGettingConversationToolTranslation : BaseGiven
    {
        private VAConversionToolTranslationDetailsResponseContract _conversionToolTranslationDetailsResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenVAConversionToolExistsAndThereIsJustOne : GivenGettingConversationToolTranslation
        {
            protected override async void When()
            {
                _conversionToolTranslationDetailsResponseContract = await SUT.GetVAConversionToolTranslation("en", PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract.FileName);
            }

            [Test]
            public void ThenResultHistoryConversionToolIsEmpty()
            {
                Assert.IsEmpty(_conversionToolTranslationDetailsResponseContract.VAConversionToolTranslations);
            }
        }

        public class WhenVAConversionToolExistsAndThereAreMany : GivenGettingConversationToolTranslation
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
                _conversionToolTranslationDetailsResponseContract = await SUT.GetVAConversionToolTranslation("en", PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract.FileName);
            }

            [Test]
            public void ThenResultHistoryConversionToolIsEmpty()
            {
                Assert.IsNotEmpty(_conversionToolTranslationDetailsResponseContract.VAConversionToolTranslations);
            }
        }

        public class WhenVAConversionToolDoesNotExist : GivenGettingConversationToolTranslation
        {
            protected override async void When()
            {
                VAConversionTools = new List<VAConversionTool>() { };
                PrepareSut();
                _conversionToolTranslationDetailsResponseContract = await SUT.GetVAConversionToolTranslation("en", PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolTranslationDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNull()
            {
                Assert.IsNull(_conversionToolTranslationDetailsResponseContract.FileName);
            }
        }
    }
}
