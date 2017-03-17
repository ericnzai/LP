using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupCommandsTests
{
    public class GivenGettingGroupsForGlossaryDropdown : BaseGiven
    {
        private GroupResponseContract _groupResponseContract;
        private string _culture = "en";

        protected override void Given()
        {
            Groups = new List<Group>
            {
                new Group(){ Name = "CORE", GroupTypeID = 1, Culture = _culture, StatusBankID = 2},
                new Group(){ Name = "AMD", GroupTypeID = 2, Culture = _culture , StatusBankID = 2},
                new Group(){ Name = "CRVO", GroupTypeID = 3, Culture = _culture, StatusBankID = 2 },
                new Group(){ Name = "BRVO", GroupTypeID = 4, Culture = _culture , StatusBankID = 2},
                new Group(){ Name = "DME", GroupTypeID = 5, Culture = _culture , StatusBankID = 2},
            };

            PrepareSut();
        }

        public class WhenTheGroupsIsRequested : GivenGettingGroupsForGlossaryDropdown
        {
            protected override async void When()
            {
                _groupResponseContract = await SUT.GetAllLiveGroupResponseContractsForGlossaryDropDown(_culture);
            }

            [Test]
            public void ThenGetAllGroupsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<Group>(It.IsAny<Expression<Func<Group, bool>>>()), Times.Once());
            }

            [Test]
            public void ThenGroupsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_groupResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupsAreReturned()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _groupResponseContract.Groups.Count);
            }

            [Test]
            public void ThenTheCorrectGroupsAreReturned()
            {
                var expected = new List<string> { "CORE", "AMD", "CRVO", "BRVO", "DME" };

                var actual = _groupResponseContract.Groups.Select(x => x.Name).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }
    }
}
