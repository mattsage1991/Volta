using System;

namespace Volta.Stocks.Domain.Stocks
{
    public static class Events
    {
        public class StockCreated
        {
            public Guid Id { get; set; }
        }

        public class StockKeyStatsUpdated
        {
            public Guid Id { get; set; }
        }
    }
    
}