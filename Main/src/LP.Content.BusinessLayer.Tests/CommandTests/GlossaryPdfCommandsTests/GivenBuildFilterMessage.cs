using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Translation;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.GlossaryPdfCommandsTests
{
    public class GivenBuildFilterMessage : BaseGiven
    {
        private string _result;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenFiltersParameterIsNull : GivenBuildFilterMessage
        {

            protected override void When()
            {
                _result = SUT.BuildFilterMessage(null, "test", "test");
            }

            [Test]
            public void ThenResultIsNull()
            {
                Assert.IsNull(_result);
            }
        }

        public class WhenFiltersParameterHasEmptySearchAndNoModules : GivenBuildFilterMessage
        {

            protected override void When()
            {
                _result = SUT.BuildFilterMessage("", "test", "test");
            }

            [Test]
            public void ThenResultIsNull()
            {
                Assert.IsNull(_result);
            }
        }

        public class WhenFiltersParameterDoesNotContainUnderscore : GivenBuildFilterMessage
        {

            protected override void When()
            {
                _result = SUT.BuildFilterMessage("test", "test", "test");
            }

            [Test]
            public void ThenResultEndsWithSemicolonAndASpace()
            {
                Assert.IsTrue(_result.EndsWith("; "));
            }
        }

        public class WhenFiltersParameterContainsOneModule : GivenBuildFilterMessage
        {

            protected override void When()
            {
                _result = SUT.BuildFilterMessage("test_module1", "test", "test");
            }

            [Test]
            public void ThenResultDoesNotContainCommaAndASpace()
            {
                Assert.IsFalse(_result.Contains("', "));
            }
        }

        //public class WhenFiltersParameterContainsMoreModules : GivenBuildFilterMessage
        //{

        //    protected override void When()
        //    {
        //        _result = SUT.BuildFilterMessage("test_module1_module2_module3", "test", "test");
        //    }

        //    [Test]
        //    public void ThenResultContainsCommaAndASpace()
        //    {
        //        Assert.IsTrue(_result.Contains("', "));
        //    }
        //}

        public class WhenFiltersIsNullAndMethodWithTranslationsIsCalled : GivenBuildFilterMessage
        {

            protected override void When()
            {
                _result = SUT.BuildFilterMessage(null, new List<TranslatedItem>());
            }

            [Test]
            public void ThenResultIsNull()
            {
                Assert.IsNull(_result);
            }
        }
    }
}
