using System;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioHoldingId : TypedIdValueBase
    {
        public PortfolioHoldingId(Guid id) : base(id)
        {
        }
    }
}