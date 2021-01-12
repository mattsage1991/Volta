using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Stocks.Application.Commands.CreateStock
{
    public class CreateStockCommand : ICommand<Guid>
    {
        public CreateStockCommand(string companyName, string tickerSymbol)
        {
            CompanyName = companyName;
            TickerSymbol = tickerSymbol;
        }

        public string CompanyName { get; }
        public string TickerSymbol { get; }
    }
}