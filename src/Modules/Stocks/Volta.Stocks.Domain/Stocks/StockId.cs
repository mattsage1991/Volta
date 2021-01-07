using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Stocks.Domain.Stocks
{
    public class StockId : TypedIdValueBase
    {
        public StockId(Guid id) : base(id)
        {
        }
    }
}