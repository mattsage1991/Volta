using System;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class HoldingId : TypedIdValueBase
    {
        public HoldingId(Guid id) : base(id)
        {
        }
    }
}