using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;

namespace Volta.Portfolios.Domain.Stocks
{
    public class Stock : Entity<StockId>
    {
        private string _tickerSymbol;
        private decimal _closePrice;

        private Stock()
        {
        }
    }
}