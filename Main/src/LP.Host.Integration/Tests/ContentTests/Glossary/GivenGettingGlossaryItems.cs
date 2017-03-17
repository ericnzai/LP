using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using LP.Api.Shared.Mime;
using LP.EntityModels;
using LP.Host.Integration.Behaviours.Authentication.UserInformation;
using LP.Host.Integration.Behaviours.Content.Glossary;
using LP.Host.Integration.Providers;
using LP.ServiceHost.DataContracts.Response.Content;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.ContentTests.Glossary
{
    public class GivenGettingGlossaryItems : BaseGiven
    {
        private GlossaryItemsResponseContract _glossaryItemsResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingCorrectRequestToGetADifferentCulture : GivenGettingGlossaryItems, INeedANewlyCreatedUser, INeedSomeGlossaryItemsWithAudio
        {
            public User User { get; set; }
            public string UserName { get { return "glossaryitemstester@asandk.com"; } }
            public int[] RolesIds { get { return new[] {1}; } }

            protected override void When()
            {
                Uri = UriProvider.Content.GlossaryItems;

                var header = CreateCustomAuthorizationHeader(UserName).ToList();
                header.Add(new KeyValuePair<string, string>("x-culture", "tr"));

                Response = SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                _glossaryItemsResponseContract = HttpContentBinding.DeserialiseJson<GlossaryItemsResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseStatusIsOk()
            {
                Assert.AreEqual(Response.StatusCode, HttpStatusCode.OK);
            }

            [Test]
            public void ThenTheCorrectNumberOfItemsShouldBeReturned()
            {
                const int expected = 5;

                Assert.AreEqual(expected ,_glossaryItemsResponseContract.GlossaryItems.Count);
            }

            [Test]
            public void ThenAllTheGlossaryItemsReturnedAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_glossaryItemsResponseContract.GlossaryItems);
            }

            [Test]
            public void ThenTheCorrectItemsShouldBeReturned()
            {
                var expected = new List<string> { "Turkish One", "Turkish Two", "Turkish Three", "Turkish Four", "Turkish Five" };

                CollectionAssert.AreEquivalent(expected, _glossaryItemsResponseContract.GlossaryItems.Select(t => t.Title));
            }

            public List<ltl_HoverOver> GlossaryItems { get{
                return new List<ltl_HoverOver>
            {
                new ltl_HoverOver{Culture = "tr", Title = "Turkish One", Description = "Description Goes here", DateCreated = DateTime.UtcNow, Word = "Turkish word 1"},
                new ltl_HoverOver{Culture = "tr", Title = "Turkish Two", Description = "Description Goes here", DateCreated = DateTime.UtcNow, Word = "Turkish word 2"},
                new ltl_HoverOver{Culture = "tr", Title = "Turkish Three", Description = "Description Goes here", DateCreated = DateTime.UtcNow, Word = "Turkish word 3"},
                new ltl_HoverOver{Culture = "tr", Title = "Turkish Four", Description = "Description Goes here", DateCreated = DateTime.UtcNow, Word = "Turkish word 4"},
                new ltl_HoverOver{Culture = "tr", Title = "Turkish Five", Description = "Description Goes here", DateCreated = DateTime.UtcNow, Word = "Turkish word 5"}
            };}
            }
            public List<ltl_HoverOver> InitialisedGlossaryItems { get; set; }
        }
    }
}
