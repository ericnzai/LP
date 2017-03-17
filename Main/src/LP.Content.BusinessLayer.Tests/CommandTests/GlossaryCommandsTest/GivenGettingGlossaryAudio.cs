using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;
using NUnit.Util;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryCommandsTest
{
    public class GivenGettingGlossaryAudio : BaseGiven
    {
        private GlossaryAudioResponseContract _glossaryAudioResponseContract;

        protected override void Given()
        {
            GlossaryItems = new List<ltl_HoverOver>
            {
                HoverOverWithEnabledAudio, HoverOverWithDisabledAudio
            };

            PrepareSut();
        }

        public class WhenTheGlossaryAudioItemExistsAndIsEnabled : GivenGettingGlossaryAudio
        {
            protected override async void When()
            {
                _glossaryAudioResponseContract = await SUT.GetGlossaryAudio(ExistingHoverOverAudioId);
            }

            [Test]
            public void ThenGetConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(
                m =>
                    m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<ltl_HoverOver, bool>>>(),
                        It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_HoverOverAudio>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetByIdAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_HoverOverAudio>(It.Is<int>(x => x == ExistingHoverOverAudioId)), Times.Once());
            }

            [Test]
            public void ThenGlossaryAudioResponseContractIsNotNull()
            {
                Assert.IsNotNull(_glossaryAudioResponseContract);
            }

            [Test]
            public void ThenFileNameIsReturnedCorrectly()
            {
                const string expected = "Test glossary.mp3";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.FileName);
            }

            [Test]
            public void ThenSourceFileByteArrayIsReturnedCorrectly()
            {
                const string expected = "data:audio/mp3;base64,AQIFCA==";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.AudioBase64);
            }

            [Test]
            public void ThenIsEnabledIsSetToTrue()
            {
                Assert.True(_glossaryAudioResponseContract.IsEnabled);
            }
        }

        public class WhenTheGlossaryAudioItemExistsAndIsNotEnabled : GivenGettingGlossaryAudio
        {
            protected override async void When()
            {
                _glossaryAudioResponseContract = await SUT.GetGlossaryAudio(ExistingDisabledHoverOverAudioId);
            }

            [Test]
            public void ThenGetConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(
                m =>
                    m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<ltl_HoverOver, bool>>>(),
                        It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_HoverOverAudio>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetByIdAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_HoverOverAudio>(It.Is<int>(x => x == ExistingDisabledHoverOverAudioId)), Times.Once());
            }

            [Test]
            public void ThenGlossaryAudioResponseContractIsNotNull()
            {
                Assert.IsNotNull(_glossaryAudioResponseContract);
            }

            [Test]
            public void ThenFileNameIsReturnedCorrectly()
            {
                const string expected = "Test glossary Disabled.mp3";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.FileName);
            }

            [Test]
            public void ThenSourceFileByteArrayIsReturnedCorrectly()
            {
                const string expected = "data:audio/mp3;base64,AQIFCA==";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.AudioBase64);
            }

            [Test]
            public void ThenIsEnabledIsSetToFalse()
            {
                Assert.False(_glossaryAudioResponseContract.IsEnabled);
            }
        }

        public class WhenTheGlossaryAudioItemDoesNotExist : GivenGettingGlossaryAudio
        {
            protected override async void When()
            {
                _glossaryAudioResponseContract = await SUT.GetGlossaryAudio(NonExistantHoverOverAudioId);
            }

            [Test]
            public void ThenGetConditionalWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(
                m =>
                    m.GetConditionalWithIncludesAsync(It.IsAny<Expression<Func<ltl_HoverOver, bool>>>(),
                        It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGetByIdAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<ltl_HoverOverAudio>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGlossaryAudioResponseContractIsNull()
            {
                Assert.IsNull(_glossaryAudioResponseContract);
            }
        }
    }
}
