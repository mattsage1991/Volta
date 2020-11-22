using System;

namespace Volta.Stocks.Domain.Stocks
{
    public class StockId
    {
        private readonly Guid _value;

        public StockId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Stock id cannot be empty");

            _value = value;
        }
    }
}