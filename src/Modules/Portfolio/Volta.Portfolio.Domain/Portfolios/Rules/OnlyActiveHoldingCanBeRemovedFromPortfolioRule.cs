using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;

namespace Volta.Portfolios.Domain.Portfolios.Rules
{
    public class OnlyActiveHoldingCanBeRemovedFromPortfolioRule : IBusinessRule
    {
        private readonly StockId _stockId;

        private readonly List<PortfolioHolding> _holdings;

        public OnlyActiveHoldingCanBeRemovedFromPortfolioRule(StockId stockId, List<PortfolioHolding> holdings)
        {
            _stockId = stockId;
            _holdings = holdings;
        }

        public bool IsBroken() => _holdings.SingleOrDefault(x => x.IsActive()) == null;

        public string Message => "Only active holdings can be removed from meeting";
    }
}