using System;
using System.Collections.Generic;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolCommandsTests
{
    public class GivenSavingAConversionTool : BaseGiven
    {
        public VAConversionToolResponseContract _conversionToolResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }
    }

    public class WhenAConversionToolIsAddedFirstTime : GivenSavingAConversionTool
    {
        protected override async void When()
        {
            _conversionToolResponseContract = await SUT.SaveVAConversionTool("en", "test2.pdf", "C:/temp/test2.pdf", "comment 2");
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
            Assert.IsNotNull(_conversionToolResponseContract);
        }

        [Test]
        public void ThenConversionToolResponseHistoryIsEmpty()
        {
            Assert.IsEmpty(_conversionToolResponseContract.ConversionToolHistory);
        }
    }

    public class WhenAConversionToolIsAddedAndThereAreSeveralOldOns : GivenSavingAConversionTool
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

            _conversionToolResponseContract = await SUT.SaveVAConversionTool("en", "test2.pdf", "C:/temp/test2.pdf", "comment 2");
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
            Assert.IsNotNull(_conversionToolResponseContract);
        }

        [Test]
        public void ThenConversionToolResponseHistoryIsNotEmpty()
        {
            Assert.IsNotEmpty(_conversionToolResponseContract.ConversionToolHistory);
        }

        [Test]
        public void ThenConversionToolResponseHistoryHasExactlyExpected()
        {
            const int expected = 2;
            Assert.IsTrue(_conversionToolResponseContract.ConversionToolHistory.Count.Equals(expected));
        }
    }
}
