//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Net;
//using System.Net.Mime;
//using System.Text;
//using System.Threading.Tasks;
//using LP.Api.Shared.Mime;
//using LP.Host.Integration.Actions;
//using LP.Host.Integration.Behaviours;
//using LP.Host.Integration.Providers;
//using LP.ServiceHost.DataContracts.Response.Authentication;
//using NUnit.Framework;
//using StructureMap;

//namespace LP.Host.Integration.AuthenticationTests.UserInformation
//{
//    [TestFixture]
//    [ConsoleAction("Hello")]
//    [ConsoleAction("Greetings")]
//    public class GivenGettingInformationForAdminUser : BaseGiven
//    {
//        private UserInformationResponseContract _userInformationResponseContract;

//        protected override void AfterEachTest()
//        {
//            Debug.WriteLine("AfterEachTest");
//        }

//        protected override void BeforeEachTest()
//        {
//            Debug.WriteLine("BeforeEachTest");
//        }

//        protected override void AfterSpec()
//        {
//            Debug.WriteLine("AfterSpec");
//        }

//        protected override void ConfigureContainer(IContainer container)
//        {
//            Debug.WriteLine("ConfigureContainer");
//        }

//        protected override void Given()
//        {
//            Debug.WriteLine("Given");
//        }

//        protected override void InitializeClassUnderTest()
//        {
//            Debug.WriteLine("InitializeClassUnderTest");
//        }

//        public override void SetupEachSpec()
//        {
//            Debug.WriteLine("SetupEachSpec");
//        }

//        public override void TearDown()
//        {
//            Debug.WriteLine("TearDown");
//        }

//        [ConsoleAction("Hello")]
//        [ConsoleAction("Greetings")]
//        public class WhenIAmTestingNunitAttributes : GivenGettingInformationForAdminUser, INeedDummyData
//        {
//            protected override void When()
//            {
//                Debug.WriteLine("When WhenIAmTestingNunitAttributes");
//            }

//            [Test]
//            public void ThenX()
//            {
//                Debug.WriteLine("Then For WhenIAmTestingNunitAttributes X");
//            }

//            [Test]
//            public void ThenY()
//            {
//                Debug.WriteLine("Then For WhenIAmTestingNunitAttributes Y");
//            }

//            public IEnumerable<Foo> Foos { get; set; }

//            [Test]
//            public void TheFoosIsNotNull()
//            {
//                Assert.IsNotNull(Foos);
//            }
//        }

//        [ConsoleAction("Hello")]
//        [ConsoleAction("Greetings")]
        
//        public class WhenIAmTestingNunitAttributesAgain : GivenGettingInformationForAdminUser
//        {
//            //private bool _result;
            
//            protected override void When()
//            {
//                //_result = SUT.BaseAddress.AbsolutePath == "X";

//                //Uri = UriProvider.Authentication.UserInformation;

//                //var header = base.AdminAuthorisationHeader;// await CreateCustomAuthorizationHeader(UserName);

//                //Response = await SUT.GetAsync(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

//                _userInformationResponseContract = new UserInformationResponseContract();// await HttpContentBinding.DeserialiseJson<UserInformationResponseContract>(Response.Content);

//                Debug.WriteLine("When WhenIAmTestingNunitAttributesAgain");
//            }

//            [Test]
//            public void ThenX()
//            {
//                Debug.WriteLine("Then For WhenIAmTestingNunitAttributesAgain");
//                Assert.IsNotNull(_userInformationResponseContract);
//            }

//            [Test]
//            public void ThenY()
//            {
               

//                Debug.WriteLine("Then For WhenIAmTestingNunitAttributesAgain");

//                Assert.AreEqual(HttpStatusCode.PaymentRequired, Response.StatusCode);
//            }
//        }
//    }
//}
