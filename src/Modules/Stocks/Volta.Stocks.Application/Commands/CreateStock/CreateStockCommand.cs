using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Stocks.Application.Commands.CreateStock
{
    public class CreateStockCommand : ICommand<Guid>
    {
        public CreateStockCommand(string companyName, string ticketSymbol)
        {
            CompanyName = companyName;
            TicketSymbol = ticketSymbol;
        }

        public string CompanyName { get; }
        public string TicketSymbol { get; }
    }
}