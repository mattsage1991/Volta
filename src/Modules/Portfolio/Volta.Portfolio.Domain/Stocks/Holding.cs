using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;

namespace Volta.Portfolios.Domain.Stocks
{
    public class Holding : Entity
    {
        public HoldingId Id { get; private set; }

        private string _tickerSymbol;
        private decimal _closePrice;

        private Holding()
        {
        }
    }
}