using System;
using MediatR;
using Volta.BuildingBlocks.Application;

namespace Volta.Stocks.Application.Commands.UpdateStock
{
    public class UpdateStockCommand : ICommand<Unit>
    {
        public UpdateStockCommand(Guid stockId)
        {
            StockId = stockId;
        }

        public Guid StockId { get; }
    }
}