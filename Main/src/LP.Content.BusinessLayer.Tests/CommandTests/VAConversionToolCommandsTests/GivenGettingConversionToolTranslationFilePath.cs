using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.VAConversionToolCommandsTests
{
    public class GivenGettingConversionToolTranslationFilePath : BaseGiven
    {
        private VAConversionToolDownloadPdfResponseContract _conversionToolDownloadPdfResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenVAConversionToolTranslationExists : GivenGettingConversionToolTranslationFilePath
        {
            protected override async void When()
            {
                _conversionToolDownloadPdfResponseContract = await SUT.GetConversionToolTranslationFilePath("en", PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<VAConversionTool>(), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDownloadPdfResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNotNullOrEmpty()
            {
                Assert.IsNotNullOrEmpty(_conversionToolDownloadPdfResponseContract.FileName);
            }
        }

        public class WhenVAConversionToolTranslationDoesNotExists : GivenGettingConversionToolTranslationFilePath
        {
            protected override async void When()
            {
                _conversionToolDownloadPdfResponseContract = await SUT.GetConversionToolTranslationFilePath("ru", PermPath);
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<VAConversionTool>(), Times.Once());
            }

            [Test]
            public void ThenResultIsNotNull()
            {
                Assert.IsNotNull(_conversionToolDownloadPdfResponseContract);
            }

            [Test]
            public void ThenResultFileNameIsNullEmpty()
            {
                Assert.IsNullOrEmpty(_conversionToolDownloadPdfResponseContract.FileName);
            }
        }
    }
}
