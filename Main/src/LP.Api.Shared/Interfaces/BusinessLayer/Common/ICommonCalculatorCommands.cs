namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface ICommonCalculatorCommands
    {
        int CalculatePercentages(int numberOfItems, int totalItems);
        int GetPagingNumberToSkip(int currentPageNumber, int numberOfItemsPerPage);
    }
}
