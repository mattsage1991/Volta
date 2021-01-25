using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Stocks
{
    public class StockId : TypedIdValueBase
    {
        public StockId(Guid value) : base(value)
        {
        }
    }
}