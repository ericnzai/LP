using System.Collections.Generic;
using System.Linq;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Translation;
using NUnit.Framework;

namespace LP.Translation.BusinessLayer.Tests.Commands.TranslationCommandsTests
{
    public class GivenGettingStaticTranslations : BaseGiven
    {
        private TranslationResponseContract _translationResponseContract;
        
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheCultureRequestedIsRussianAndTheResourceDoesntExistForThatCulture : GivenGettingStaticTranslations
        {
            protected override async void When()
            {
                TranslationRequestContract.Culture = "ru";

                TranslationRequestContract.TranslationRequests = new List<TranslationRequest>
                {
                    new TranslationRequest {ResourceId = "ResId3", ResourceSet = "ResSet1"}
                };

                _translationResponseContract = await SUT.GetTranslatedItems(TranslationRequestContract);
            }

            [Test]
            public void ThenTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_translationResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfTranslationsAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.Count);
            }

            [Test]
            public void ThenTheCorrectGlobalTranslationIsReturned()
            {
                const string expected = "ResId3 ResSet1 global";

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.First().TranslatedValue);
            }
        }

        public class WhenTheCultureRequestedIsEnglishAndTheResourceExistsForThatCulture : GivenGettingStaticTranslations
        {
            protected override async void When()
            {
                TranslationRequestContract.Culture = "en";

                TranslationRequestContract.TranslationRequests = new List<TranslationRequest>
                {
                    new TranslationRequest {ResourceId = "ResId4", ResourceSet = "ResSet1"}
                };

                _translationResponseContract = await SUT.GetTranslatedItems(TranslationRequestContract);
            }

            [Test]
            public void ThenTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_translationResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfTranslationsAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.Count);
            }

            [Test]
            public void ThenTheCorrectEnglishTranslationIsReturned()
            {
                const string expected = "ResId4 ResSet1 en";

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.First().TranslatedValue);
            }
        }

        public class WhenTheCultureRequestedIsEnglishAndTheResourceDoesNotExistForAnyCulture : GivenGettingStaticTranslations
        {
            private const string NonExistantResourceId = "NonExistant";

            protected override async void When()
            {
                TranslationRequestContract.Culture = "en";

                TranslationRequestContract.TranslationRequests = new List<TranslationRequest>
                {
                    new TranslationRequest {ResourceId = NonExistantResourceId, ResourceSet = "ResSet1"}
                };

                _translationResponseContract = await SUT.GetTranslatedItems(TranslationRequestContract);
            }

            [Test]
            public void ThenTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_translationResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfTranslationsAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.Count);
            }

            [Test]
            public void ThenTheResourceIdIsReturned()
            {
                const string expected = NonExistantResourceId;

                Assert.AreEqual(expected, _translationResponseContract.TranslatedItems.First().TranslatedValue);
            }
        }
    }
}
