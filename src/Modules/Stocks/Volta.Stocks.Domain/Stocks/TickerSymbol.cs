using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class TickerSymbol : ValueObject
    {
        public string Value { get; }

        private TickerSymbol() { }

        private TickerSymbol(string value)
        {
            this.Value = value;
        }

        public static TickerSymbol Of(string value)
        {
            return new TickerSymbol(value);
        }
    }
}