using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Models;

namespace Volta.Stocks.Application.Commands.UpdateStocks
{
    public class UpdateStocksCommand : ICommand<StocksUpdatedModel>, IRecurringCommand
    {
        public UpdateStocksCommand()
        {
            
        }
    }
}