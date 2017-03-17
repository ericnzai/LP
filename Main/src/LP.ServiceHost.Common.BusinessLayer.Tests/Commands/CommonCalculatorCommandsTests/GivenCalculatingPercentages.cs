using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CommonCalculatorCommandsTests
{
    public class GivenCalculatingPercentages : BaseGiven
    {
        private int _percentageReturned;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheResultShouldBeThirtyFive : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = 35;

                const int totalNumberOfItems = 100;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 35;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenTheResultShouldBeFourty : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = 4;

                const int totalNumberOfItems = 10;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 40;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenTheResultShouldBeSixty : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = 60;

                const int totalNumberOfItems = 100;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 60;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenPassingInZeroAsTheNumberOfItems : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = 0;

                const int totalNumberOfItems = 100;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenRequestIsMadeWithMaxIntValues : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = int.MaxValue;

                const int totalNumberOfItems = int.MaxValue;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 100;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenRequestIsMadeWithMaxValueAndZero : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = 0;

                const int totalNumberOfItems = int.MaxValue;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenRequestIsMadeWithMaxValueAndHalfOfMaxValue : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = int.MaxValue / 2;

                const int totalNumberOfItems = int.MaxValue;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 49;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenRequestIsMadeWithNegativeNumberOfItems : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = -1;

                const int totalNumberOfItems = int.MaxValue;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }

        public class WhenRequestIsMadeWithNegativeTotalNumberOfItems : GivenCalculatingPercentages
        {
            protected override void When()
            {
                const int numberOfItems = int.MaxValue;

                const int totalNumberOfItems = -2;

                _percentageReturned = SUT.CalculatePercentages(numberOfItems, totalNumberOfItems);
            }

            [Test]
            public void ThenThePercentageReturnedIsCorrect()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _percentageReturned);
            }
        }
    }
}
