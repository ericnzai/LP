using System;
using System.Collections.Generic;
using System.Linq;
using LP.EntityModels.StoredProcedure.Input;
using LP.EntityModels.StoredProcedure.Output;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.SearchCommandsTests
{
    public class GivenGettingSearchItems : BaseGiven
    {
        private SearchItemsResponseContract _searchItemsResponseContract;

        protected override void Given()
        {
            SearchItems = new List<SearchWithRowCount>
            {
                new SearchWithRowCount()
                {
                    Name = "test", Body = "test body", GroupName = "DME", LastUpdated = new DateTime(), ParentSectionName = "Pathophysiology and natural history of DME", Subject = "subject"
                },
                new SearchWithRowCount()
                {
                    Name = "test", Body = "test body 1", GroupName = "CORE", LastUpdated = new DateTime(), ParentSectionName = "Pathophysiology and natural history of DME", Subject = "subject 1"
                },
                new SearchWithRowCount()
                {
                    Name = "test", Body = "test body 2", GroupName = "BRVO", LastUpdated = new DateTime(), ParentSectionName = "Pathophysiology and natural history of DME", Subject = "subject 2"
                }
            };

            PrepareSut();
        }

        public class WhenTheSearchItemsIsRequested : GivenGettingSearchItems
        {
            protected override async void When()
            {
                _searchItemsResponseContract = await SUT.GetAllSearchItems("en", "test", new List<int>(){1,2},"0", "0");
            }

            [Test]
            public void ThenExecuteStoredProcedureIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.ExecuteStoredProcedure<SearchWithRowCount, ltl_SearchWithRowCountArguments>(It.IsAny<ltl_SearchWithRowCountArguments>()), Times.Once());
            }

            [Test]
            public void ThenSearchItemsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_searchItemsResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfSearchItemsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _searchItemsResponseContract.SearchItems.Count);
            }

            [Test]
            public void ThenTheCorrectGlossaryItemsAreReturned()
            {
                var expected = new List<string> { "subject", "subject 1", "subject 2" };

                var actual = _searchItemsResponseContract.SearchItems.Select(x => x.Title).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }
    }
}
