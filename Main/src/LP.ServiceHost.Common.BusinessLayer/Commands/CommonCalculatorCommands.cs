using LP.Api.Shared.Interfaces.BusinessLayer.Common;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class CommonCalculatorCommands : ICommonCalculatorCommands
    {
        public int CalculatePercentages(int numberOfItems, int totalItems)
        {
            if (numberOfItems <= 0 || totalItems <= 0) return 0;

            var percentageCalculation = ((decimal)numberOfItems / totalItems) * 100;

            return (int)percentageCalculation;
        }

        public int GetPagingNumberToSkip(int currentPageNumber, int numberOfItemsPerPage)
        {
            return (currentPageNumber - 1)*numberOfItemsPerPage;
        }
    }
}
