using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Rules
{
    public class StockCannotBeAHoldingOfPortfolioMoreThanOnce : IBusinessRule
    {
        private readonly StockId _stockId;

        private readonly List<PortfolioHolding> _holdings;

        public StockCannotBeAHoldingOfPortfolioMoreThanOnce(StockId stockId, List<PortfolioHolding> holdings)
        {
            _stockId = stockId;
            _holdings = holdings;
        }

        public bool IsBroken() => _holdings.SingleOrDefault(x => x.IsActive()) != null;

        public string Message => "Stock is already a holding in this portfolio";
    }
}