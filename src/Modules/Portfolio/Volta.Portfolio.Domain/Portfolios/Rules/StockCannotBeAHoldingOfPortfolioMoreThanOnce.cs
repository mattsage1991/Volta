using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Rules
{
    public class StockCannotBeAHoldingOfPortfolioMoreThanOnce : IBusinessRule
    {
        private readonly StockId stockId;

        private readonly List<Holding> _holdings;

        public StockCannotBeAHoldingOfPortfolioMoreThanOnce(StockId stockId, List<Holding> holdings)
        {
            this.stockId = stockId;
            _holdings = holdings;
        }

        public bool IsBroken() => _holdings.SingleOrDefault(x => x.IsActive()) != null;

        public string Message => "Stock is already a holding in this portfolio";
    }
}