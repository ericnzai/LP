using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.GroupTypeCommandsTests
{
    public class GivenGettingAllAvailableGroupTypes : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingAllAvailableGroupTypesAsAList : GivenGettingAllAvailableGroupTypes
        {
            private IEnumerable<ltl_GroupType> _groupTypes;

            protected override async void When()
            {
                _groupTypes = await SUT.GetAllAvailableGroupTypes();
            }

            [Test]
            public void ThenTrainingAreaCommandsGetLiveTrainingAreasWithIncludesIsCalledOnce()
            {
                TrainingAreaCommandsMock.Verify(m => m.GetLiveTrainingAreasWithIncludes(), Times.Once());
            }

            [Test]
            public void ThenGroupTypesIsNotNull()
            {
                Assert.IsNotNull(_groupTypes);
            }

            [Test]
            public void ThenTheCorrectNumberOfGroupTypesAreReturned()
            {
                const int expected = 8;

                Assert.AreEqual(expected, _groupTypes.Count());
            }

            [Test]
            public void ThenTheCorrectGroupTypeIdsAreReturned()
            {
                var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

                CollectionAssert.AreEquivalent(expected, _groupTypes.Select(a => a.ID));
            }
        }       
    }
}
