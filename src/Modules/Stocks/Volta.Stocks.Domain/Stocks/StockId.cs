using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class StockId : Value<StockId>
    {
        public readonly Guid Value;

        public StockId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Stock id cannot be empty");

            Value = value;
        }

        public static StockId Of(Guid id) => new StockId(id);

        public static implicit operator Guid(StockId self) => self.Value;
    }
}