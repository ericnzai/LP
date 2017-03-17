using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using LP.Api.Shared.Mime;
using LP.Host.Integration.Providers;
using LP.ServiceHost.DataContracts.Response.Content;
using LP.ServiceHost.DataContracts.Response.Exams;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.ExamsTests
{
    public class GivenGettingTheOverviewGroupProgress : BaseGiven
    {
        private OverviewGroupTypeProgressResponseContract _overviewGroupTypeProgressResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingFirstGroupTypeAsAnAdminUser : GivenGettingTheOverviewGroupProgress
        {
            protected override void When()
            {
                Uri = string.Format(UriProvider.Exams.OverviewGroupTypeProgress, 1);

                var header = CreateCustomAuthorizationHeader().ToList();
                
                header.Add(new KeyValuePair<string, string>("x-culture", "en"));

                Response = SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                HttpContentBinding.CheckResponse(Response);

                _overviewGroupTypeProgressResponseContract = HttpContentBinding.DeserialiseJson<OverviewGroupTypeProgressResponseContract>(Response.Content);
            }

            [Test]
            public void ThenGroupTypeNameIsCorrect()
            {
                Assert.True( _overviewGroupTypeProgressResponseContract.DashboardBarChartContracts.Select(a => a.Title).Contains("CORE"));
            }
        }
    }
}
