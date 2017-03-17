using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryCommandsTest
{
    public class GivenGettingGlossaryItems : BaseGiven
    {
        private GlossaryItemsResponseContract _glossaryItemsResponseContract;

        protected override void Given()
        {
            GlossaryItems = new List<ltl_HoverOver>
            {
                new ltl_HoverOver {HoverOverID = 1, AudioFileID = 11, ltl_HoverOverAudio = new ltl_HoverOverAudio(){FileName = "test", HoverOverAudioID = 11, IsEnabled = true}, Title = "Visual acuity", Description = "The measure of acuteness or clarity of vision", Culture = "en"},
                new ltl_HoverOver {HoverOverID = 2, AudioFileID = null, Title = "Optometrist", Description = "In general, optometrists dispense and fit glasses and contact lenses. In some countries, such as the UK, Canada, and Australia, they will additionally be the professionals who typically test and screen the eyes to detect certain eye abnormalities, prescribe a limited range of medicines, or refer patients on to an ophthalmologist<br />", Culture = "en"},
                new ltl_HoverOver {HoverOverID = 3, AudioFileID = 13, ltl_HoverOverAudio = new ltl_HoverOverAudio(){FileName = "test", HoverOverAudioID = 13, IsEnabled = true}, Title = "Neovascularization", Description = "The formation of new blood vessels from pre-existing vasculature", Culture = "en"},
                new ltl_HoverOver {HoverOverID = 4, AudioFileID = null, Title = "Refractive condition", Description = "Optical defect that prevents light rays from being brought to a single focus on the retina, leading to blurred vision. The three most common refractive errors are myopia (nearsightedness), hyperopia (farsightedness), and astigmatism (focus problems caused by the cornea)", Culture = "en"},
                new ltl_HoverOver {HoverOverID = 5, AudioFileID = 15, ltl_HoverOverAudio = new ltl_HoverOverAudio(){FileName = "test", HoverOverAudioID = 15, IsEnabled = true}, Title = "hyperopia", Description = "Condition in which visual images come to a focus behind the retina of the eye and vision is better for distant than for near objects (often referred to as &lsquo;far-sighted&rsquo;)", Culture = "en"}
            };

            PrepareSut();
        }

        public class WhenTheGlossaryItemsIsRequested : GivenGettingGlossaryItems
        {
            protected override async void When()
            {
                _glossaryItemsResponseContract = await SUT.GetAllGlossaryItems("en");
            }

            [Test]
            public void ThenGetAllGlossaryItemsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<ltl_HoverOver, object>>[]>()), Times.Once());
            }

            [Test]
            public void ThenGlossaryItemsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_glossaryItemsResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfGlossaryItemsAreReturned()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _glossaryItemsResponseContract.GlossaryItems.Count);
            }

            [Test]
            public void ThenTheCorrectGlossaryItemsAreReturned()
            {
                var expected = new List<string> { "Visual acuity", "Optometrist", "Neovascularization", "Refractive condition", "hyperopia" };

                var actual = _glossaryItemsResponseContract.GlossaryItems.Select(x => x.Title).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }

            [Test]
            public void ThenTheCorrectIdsAreReturned()
            {
                var expected = new List<int> {1,2,3,4,5 };

                var actual = _glossaryItemsResponseContract.GlossaryItems.Select(x => x.GlossaryItemId).ToList();

                CollectionAssert.AreEquivalent(expected, actual);   
            }
        }

        public class WhenRequestingGlossaryItemsThatHaveAudio : GivenGettingGlossaryItems
        {
            protected override async void When()
            {
                _glossaryItemsResponseContract = await SUT.GetAllGlossaryItems("en");
            }

            [Test]
            public void ThenTheCorrectNumberOfGlossaryItemsHaveAudio()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _glossaryItemsResponseContract.GlossaryItems.Count(a => a.HasAudio));
            }

            [Test]
            public void ThenTheCorrectNumberOfGlossaryItemsDoNotHaveAudio()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _glossaryItemsResponseContract.GlossaryItems.Count(a => !a.HasAudio));
            }

            [Test]
            public void ThenTheCorrectGlossaryItemsHaveAudio()
            {
                var expected = new List<int> { 1,3,5 };

                CollectionAssert.AreEquivalent(expected, _glossaryItemsResponseContract.GlossaryItems.Where(a => a.HasAudio).Select(i => i.GlossaryItemId).ToList());
            }

            [Test]
            public void ThenTheCorrectGlossaryItemsDoNotHaveAudio()
            {
                var expected = new List<int> { 2,4 };

                CollectionAssert.AreEquivalent(expected, _glossaryItemsResponseContract.GlossaryItems.Where(a => !a.HasAudio).Select(i => i.GlossaryItemId).ToList());
            }
        }
    }
}
