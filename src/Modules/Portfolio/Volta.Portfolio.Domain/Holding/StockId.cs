using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Holding
{
    public class StockId : TypedIdValueBase
    {
        public StockId(Guid value) : base(value)
        {
        }
    }
}