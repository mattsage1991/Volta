using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Stocks
{
    public class HoldingId : TypedIdValueBase
    {
        public HoldingId(Guid value) : base(value)
        {
        }
    }
}