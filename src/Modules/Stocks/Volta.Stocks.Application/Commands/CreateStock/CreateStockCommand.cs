using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Stocks.Application.Commands.CreateStock
{
    public class CreateStockCommand : ICommand<Guid>
    {
        public CreateStockCommand(string companyName, string symbol)
        {
            CompanyName = companyName;
            Symbol = symbol;
        }

        public string CompanyName { get; }
        public string Symbol { get; }
    }
}