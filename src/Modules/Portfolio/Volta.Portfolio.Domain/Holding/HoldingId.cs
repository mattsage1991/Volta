using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Holding
{
    public class HoldingId : TypedIdValueBase
    {
        public HoldingId(Guid value) : base(value)
        {
        }
    }
}