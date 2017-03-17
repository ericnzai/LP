using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolCommandsTests
{
    public class GivenGettingVAConversionTool : BaseGiven
    {
        private VAConversionToolDetailsResponseContract _conversionToolDetailsResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenVAConversionToolExistsAndThereIsJustOne : GivenGettingVAConversionTool
        {
            protected override async void When()
            {
                _conversionToolDetailsResponseContract = await SUT.GetVAConversionTool("en", UserDetail, PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDetailsResponseContract.FileName);
            }

            [Test]
            public void ThenResultHistoryConversionToolIsEmpty()
            {
                Assert.IsEmpty(_conversionToolDetailsResponseContract.VAConversionTools);
            }
        }


        public class WhenVAConversionToolExistsAndThereAreMany : GivenGettingVAConversionTool
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
                _conversionToolDetailsResponseContract = await SUT.GetVAConversionTool("en", UserDetail, PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDetailsResponseContract.FileName);
            }

            [Test]
            public void ThenResultHistoryConversionToolIsNotEmpty()
            {
                Assert.IsNotEmpty(_conversionToolDetailsResponseContract.VAConversionTools);
            }
        }

        public class WhenVAConversionToolDoesNotExist : GivenGettingVAConversionTool
        {
            protected override async void When()
            {
                VAConversionTools = new List<VAConversionTool>() { };
                PrepareSut();
                _conversionToolDetailsResponseContract = await SUT.GetVAConversionTool("en", UserDetail, PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<VAConversionTool>(It.IsAny<Expression<Func<VAConversionTool, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDetailsResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNull()
            {
                Assert.IsNull(_conversionToolDetailsResponseContract.FileName);
            }
        }
    }
}
