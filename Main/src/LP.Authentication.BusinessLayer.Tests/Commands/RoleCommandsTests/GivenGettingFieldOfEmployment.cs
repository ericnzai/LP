using System.Collections.Generic;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.RoleCommandsTests
{
    public class GivenGettingFieldOfEmployment: BaseGiven
    {
        private string _fieldOfEmployment;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserDoesNotHaveAFieldOfEmploymentSet : GivenGettingFieldOfEmployment
        {
            protected override async void When()
            {
                _fieldOfEmployment = await SUT.GetFieldOfEmployment(new List<int>());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenFieldOfEmploymentIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_fieldOfEmployment));
            }

            [Test]
            public void ThenTheCorrectFieldOfEmploymentIsReturned()
            {
                const string expected = "Other";
                    
                Assert.AreEqual(expected, _fieldOfEmployment);
            }
        }

        public class WhenTheUserHasOnlyASingleRoleIdOfMsl : GivenGettingFieldOfEmployment
        {
            protected override async void When()
            {
                _fieldOfEmployment = await SUT.GetFieldOfEmployment(new List<int>{1});
            }

            [Test]
            public void ThenTheCorrectFieldOfEmploymentIsReturned()
            {
                const string expected = "MSL";

                Assert.AreEqual(expected, _fieldOfEmployment);
            }
        }

        public class WhenTheUserHasOnlyASingleRoleIdOfMedical : GivenGettingFieldOfEmployment
        {
            protected override async void When()
            {
                _fieldOfEmployment = await SUT.GetFieldOfEmployment(new List<int> { 2 });
            }

            [Test]
            public void ThenTheCorrectFieldOfEmploymentIsReturned()
            {
                const string expected = "Medical";

                Assert.AreEqual(expected, _fieldOfEmployment);
            }
        }

        public class WhenTheUserHasOnlyASingleRoleIdOfCommercial : GivenGettingFieldOfEmployment
        {
            protected override async void When()
            {
                _fieldOfEmployment = await SUT.GetFieldOfEmployment(new List<int> { 3 });
            }

            [Test]
            public void ThenTheCorrectFieldOfEmploymentIsReturned()
            {
                const string expected = "Commercial";

                Assert.AreEqual(expected, _fieldOfEmployment);
            }
        }

        public class WhenTheUserHasOnlyASingleRoleIdOfSales : GivenGettingFieldOfEmployment
        {
            protected override async void When()
            {
                _fieldOfEmployment = await SUT.GetFieldOfEmployment(new List<int> { 4 });
            }

            [Test]
            public void ThenTheCorrectFieldOfEmploymentIsReturned()
            {
                const string expected = "Sales";

                Assert.AreEqual(expected, _fieldOfEmployment);
            }
        }
    }
}
