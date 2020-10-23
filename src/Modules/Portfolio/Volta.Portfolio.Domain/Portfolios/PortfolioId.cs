using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioId : TypedIdValueBase
    {
        public PortfolioId(Guid value) : base(value)
        {
        }
    }
}
