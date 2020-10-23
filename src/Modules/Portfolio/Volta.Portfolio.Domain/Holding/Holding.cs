using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Holding
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