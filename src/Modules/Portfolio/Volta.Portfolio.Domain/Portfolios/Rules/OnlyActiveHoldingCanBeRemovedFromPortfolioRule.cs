using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Rules
{
    public class OnlyActiveHoldingCanBeRemovedFromPortfolioRule : IBusinessRule
    {
        private readonly HoldingId _holdingId;

        private readonly List<PortfolioHolding> _holdings;

        public OnlyActiveHoldingCanBeRemovedFromPortfolioRule(HoldingId holdingId, List<PortfolioHolding> holdings)
        {
            _holdingId = holdingId;
            _holdings = holdings;
        }

        public bool IsBroken() => _holdings.SingleOrDefault(x => x.IsActive()) == null;

        public string Message => "Only active holdings can be removed from meeting";
    }
}