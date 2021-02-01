using System;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.PortfolioOwners
{
    public class PortfolioOwnerId : TypedIdValueBase
    {
        public PortfolioOwnerId(Guid id) : base(id)
        {
        }

        public static PortfolioOwnerId Of(Guid value)
        {
            return new PortfolioOwnerId(value);
        }
    }
}