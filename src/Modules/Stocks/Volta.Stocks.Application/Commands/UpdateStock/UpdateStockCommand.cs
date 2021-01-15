using System;
using Newtonsoft.Json;
using Volta.BuildingBlocks.Application;

namespace Volta.Stocks.Application.Commands.UpdateStock
{
    public class UpdateStockCommand : InternalCommandBase
    {
        public Guid StockId { get; }

        [JsonConstructor]
        public UpdateStockCommand(
            Guid id,
            Guid stockId)
            : base(id)
        {
            StockId = stockId;
        }
    }
}