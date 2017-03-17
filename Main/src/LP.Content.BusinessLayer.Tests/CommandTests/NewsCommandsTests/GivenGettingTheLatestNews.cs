using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.NewsCommandsTests
{
    public class GivenGettingTheLatestNews : BaseGiven
    {
        private LatestNewsResponseContract _latestNewsResponseContract;
        private LatestNewsItem _firstLatestNewsItem;
        private LatestNewsItem _secondLatestNewsItem;
        private LatestNewsItem _thirdLatestNewsItem;
        protected override void Given()
        {
            News = new List<News>
            {
                new News{NewsID = 1, Date = new DateTime(2010, 01,01), Title = "Title 1", BodyText = "Body Text 1", Status = (int)Status.Live},
                new News{NewsID = 2, Date = new DateTime(2017, 01,01), Title = "Title 2", BodyText = "<p><a>Body</a> Text<br/> 2</p>", Status = (int)Status.Live},
                new News{NewsID = 3, Date = new DateTime(2018, 01,01), Title = "Title 3", BodyText = "<p><a>Body</a> Text<br/> 3</p>", Status = (int)Status.Live},
                new News{NewsID = 4, Date = new DateTime(2019, 11,13), Title = "Title 4", BodyText = "<p><a>Body</a> Text<br/> 4</p>", Status = (int)Status.Live},
                new News{NewsID = 5, Date = new DateTime(2016, 01,01), Title = "Title 5", BodyText = "Body Text 5", Status = (int)Status.Live},
                new News{NewsID = 6, Date = new DateTime(2011, 01,01), Title = "Title 6", BodyText = "Body Text 6", Status = (int)Status.Live},
                new News{NewsID = 7, Date = new DateTime(2014, 01,01), Title = "Title 7", BodyText = "Body Text 7", Status = (int)Status.Deleted},
                new News{NewsID = 8, Date = new DateTime(2013, 01,01), Title = "Title 8", BodyText = "Body Text 8", Status = (int)Status.Live},
                new News{NewsID = 9, Date = new DateTime(2016, 01,01), Title = "Title 9", BodyText = "Body Text 9", Status = (int)Status.Live},
                new News{NewsID = 10, Date = new DateTime(2018, 01,01), Title = "Title 10", BodyText = "Body Text 10", Status = (int)Status.Deleted},
            };

            PrepareSut();
        }

        public class WhenTheNewsIsRequested : GivenGettingTheLatestNews
        {
            protected override async void When()
            {
                _latestNewsResponseContract = await SUT.GetLatestNewsAsync(new CultureInfo("en"));
            }

            [Test]
            public void TheGetAllNewsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<News>(), Times.Once());
            }

            [Test]
            public void ThenLatestNewsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_latestNewsResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfNewsItemsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _latestNewsResponseContract.LatestNewsItems.Count);
            }

            [Test]
            public void ThenTheCorrectNewsItemsAreReturned()
            {
                var expected = new List<int> {4,3,2};

                var actual = _latestNewsResponseContract.LatestNewsItems.Select(x => x.NewsId).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }

        public class WhenTheBodyTextContainsHtml : GivenGettingTheLatestNews
        {
            protected override async void When()
            {
                _latestNewsResponseContract = await SUT.GetLatestNewsAsync(new CultureInfo("en"));

                _firstLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 4);
                _secondLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 3);
                _thirdLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 2);
            }

            [Test]
            public void ThenFirstNewsItemBodyTextHasCorrectlyRemovedHtml()
            {
                Assert.AreEqual("Body Text 4", _firstLatestNewsItem.Content);
            }

            [Test]
            public void ThenSecondNewsItemBodyTextHasCorrectlyRemovedHtml()
            {
                Assert.AreEqual("Body Text 3", _secondLatestNewsItem.Content);
            }

            [Test]
            public void ThenThirdtNewsItemBodyTextHasCorrectlyRemovedHtml()
            {
                Assert.AreEqual("Body Text 2", _thirdLatestNewsItem.Content);
            }
        }

        public class WhenTheNewsIsRequestedWithATurkishUser : GivenGettingTheLatestNews
        {
            protected override async void When()
            {
                _latestNewsResponseContract = await SUT.GetLatestNewsAsync(new CultureInfo("tr"));

                _firstLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 4);
                _secondLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 3);
                _thirdLatestNewsItem = _latestNewsResponseContract.LatestNewsItems.First(x => x.NewsId == 2);
            }

            [Test]
            public void ThenTheFirstNewsItemDateShouldBeCorrect()
            {
                Assert.AreEqual("13 Kas 2019", _firstLatestNewsItem.Date);
            }
        }
    }
}
