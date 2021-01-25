using System;

namespace Volta.Stocks.Application.Models
{
    public class StockModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string TickerSymbol { get; set; }
    }
}