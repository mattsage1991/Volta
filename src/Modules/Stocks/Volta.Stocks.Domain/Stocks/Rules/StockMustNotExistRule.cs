using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Rules
{
    public class StockMustNotExistRule : IBusinessRule
    {
        private readonly CompanyName companyName;
        private readonly IStockExistsChecker stockExistsChecker;

        public StockMustNotExistRule(CompanyName companyName, IStockExistsChecker stockExistsChecker)
        {
            this.companyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
            this.stockExistsChecker = stockExistsChecker ?? throw new ArgumentNullException(nameof(stockExistsChecker));
        }

        public bool IsBroken()
        {
            return stockExistsChecker.Exists(companyName).GetAwaiter().GetResult();
        }

        public string Message => "Stock already exists";
    }
}