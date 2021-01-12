using System;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioId : TypedIdValueBase
    {
        public PortfolioId(Guid id) : base(id)
        {
        }
    }
}
