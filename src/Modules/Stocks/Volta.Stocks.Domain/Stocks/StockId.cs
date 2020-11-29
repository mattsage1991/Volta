using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class StockId : Value<StockId>
    {
        private readonly Guid _value;

        public StockId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Stock id cannot be empty");

            _value = value;
        }

        public static implicit operator Guid(StockId self) => self._value;
    }
}