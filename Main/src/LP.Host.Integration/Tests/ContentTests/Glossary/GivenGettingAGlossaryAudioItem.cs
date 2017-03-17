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
    public class GivenGettingAGlossaryAudioItem : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        private GlossaryAudioResponseContract _glossaryAudioResponseContract;

        public class WhenTheGlossaryItemExists : GivenGettingAGlossaryAudioItem, INeedSomeGlossaryItemsWithAudio, INeedANewlyCreatedUser
        {
            public List<ltl_HoverOver> GlossaryItems { 
                get 
                {
                    return new List<ltl_HoverOver>
                    {
                        new ltl_HoverOver
                        {
                            Word   = "Test word",
                            Description = "This is the description",
                            Title = "Title here",
                            ltl_HoverOverAudio = new ltl_HoverOverAudio
                            {
                                FileName = "TestFile.mp3",
                                IsEnabled = true,
                                SourceFile = new byte[] {0x1, 0x4, 0x7, 0x26, 0x34}
                            },
                            DateCreated = DateTime.UtcNow,
                            DateModified = DateTime.UtcNow,
                            FindPlural = true
                        }
                    };
                }
                set { value = GlossaryItems; } }
            public List<ltl_HoverOver> InitialisedGlossaryItems { get; set; }
            public User User { get; set; }

            public string UserName
            {
                get { return "glossaryaudiotest1@asandk.com"; }
            }
            
            public string Password
            {
                get { return "123Hello"; }
            }

            public int[] RolesIds
            {
                get { return new[] { 1 }; }
            }


            protected override void When()
            {
                Uri = string.Format(UriProvider.Content.GlossaryAudio, InitialisedGlossaryItems.First().HoverOverID);

                var header = CreateCustomAuthorizationHeader(UserName, Password);

                Response =  SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                _glossaryAudioResponseContract = HttpContentBinding.DeserialiseJson<GlossaryAudioResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseStatusCodeIsCorrect()
            {
                Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
            }

            [Test]
            public void ThenGlossaryItemResponseContractIsNotNull()
            {
                Assert.IsNotNull(_glossaryAudioResponseContract);
            }

            [Test]
            public void ThenFileNameIsCorrect()
            {
                const string expected = "TestFile.mp3";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.FileName);
            }

            [Test]
            public void ThenAudioBytesIsCorrect()
            {
                const string expected = "data:audio/mp3;base64,AQQHJjQ=";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.AudioBase64);
            }

            [Test]
            public void ThenGlossaryAudioItemIsEnabled()
            {
                Assert.True(_glossaryAudioResponseContract.IsEnabled);
            }
        }

        public class WhenTheGlossaryItemExistsButIsNotEnabled : GivenGettingAGlossaryAudioItem, INeedSomeGlossaryItemsWithAudio, INeedANewlyCreatedUser
        {
            public List<ltl_HoverOver> GlossaryItems
            {
                get
                {
                    return new List<ltl_HoverOver>
                    {
                        new ltl_HoverOver
                        {
                            Word   = "Test word",
                            Description = "This is the description",
                            Title = "Title here",
                            ltl_HoverOverAudio = new ltl_HoverOverAudio
                            {
                                FileName = "TestFile.mp3",
                                IsEnabled = false,
                                SourceFile = new byte[] {0x1, 0x4, 0x7}
                            },
                            DateCreated = DateTime.UtcNow,
                            DateModified = DateTime.UtcNow,
                            FindPlural = true
                        }
                    };
                }
                set { value = GlossaryItems; }
            }
            public List<ltl_HoverOver> InitialisedGlossaryItems { get; set; }
            public User User { get; set; }

            public string UserName
            {
                get { return "glossaryaudiotest2@asandk.com"; }
            }

            public int[] RolesIds
            {
                get { return new[] { 1 }; }
            }


            protected override void When()
            {
                Uri = string.Format(UriProvider.Content.GlossaryAudio, InitialisedGlossaryItems.First().HoverOverID);

                var header = CreateCustomAuthorizationHeader(UserName);

                Response = SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                _glossaryAudioResponseContract = HttpContentBinding.DeserialiseJson<GlossaryAudioResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseStatusCodeIsCorrect()
            {
                Assert.AreEqual(HttpStatusCode.NotFound, Response.StatusCode);
            }

            [Test]
            public void ThenGlossaryItemResponseContractIsNull()
            {
                Assert.IsNull(_glossaryAudioResponseContract);
            }
        }

        public class WhenTheGlossaryItemDoesNotExist : GivenGettingAGlossaryAudioItem, INeedANewlyCreatedUser //, INeedSomeGlossaryItemsWithAudio, INeedANewlyCreatedUser
        {
            public User User { get; set; }

            public string UserName
            {
                get { return "glossaryaudiotest3@asandk.com"; }
            }
            public string Password
            {
                get { return "123Hello"; }
            }

            public int[] RolesIds
            {
                get { return new[] { 1 }; }
            }


            protected override void When()
            {
                Uri = string.Format(UriProvider.Content.GlossaryAudio, int.MaxValue);

                var header = CreateCustomAuthorizationHeader(UserName, Password);

                Response = SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                _glossaryAudioResponseContract = HttpContentBinding.DeserialiseJson<GlossaryAudioResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseStatusCodeIsCorrect()
            {
                Assert.AreEqual(HttpStatusCode.NotFound, Response.StatusCode);
            }

            [Test]
            public void ThenGlossaryItemResponseContractIsNull()
            {
                Assert.IsNull(_glossaryAudioResponseContract);
            }
        }
    }
}
