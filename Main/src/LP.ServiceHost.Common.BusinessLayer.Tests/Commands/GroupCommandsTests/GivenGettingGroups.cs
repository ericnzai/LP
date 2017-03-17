using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupCommandsTests
{
    public class GivenGettingGroups : BaseGiven
    {
        private Dictionary<int, bool> _dictionary;
        private Group _group;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingGroupsThatAreLiveByIds : GivenGettingGroups
        {
            protected override async void When()
            {
    
                _dictionary = await SUT.AreLiveByIds(new List<int> { 1, 2, 3, 4, 5, 6 });
            }

            [Test]
            public void ThenDictionaryIsNotNull()
            {
                Assert.IsNotNull(_dictionary);
            }

            [Test]
            public void ThenGetAllGroupsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Group>(), Times.Once());
            }

            [Test]
            public void ThenTheCorrectNumberOfGroupsAreReturned()
            {
                Assert.AreEqual(6, _dictionary.Keys.Count);
            }

            [Test]
            public void ThenTheCorrectAmountOfLiveGroupsAreReturned()
            {
                var actual = _dictionary.Values.Count(x => x == true);
                
                const int expected = 3;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ThenTheCorrectGroupsAreReturned()
            {
                var actual = _dictionary.Where(x => x.Value).Select(k => k.Key).ToList();

                var expected = new List<int> {2,4,6};

                CollectionAssert.AreEquivalent(actual, expected);
            }
        }

        public class WhenGettingAGroupById : GivenGettingGroups
        {
            protected override async void When()
            {
                _group = await SUT.GetByIdAsync(1);
            }

            [Test]
            public void ThenGetGroupByIdIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Group>(It.IsAny<int>()), Times.Once());
            }

           
        }
    }
}
