using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioCreatedDate : ValueObject
    {
        public DateTime Value { get; }

        private PortfolioCreatedDate() { }
        private PortfolioCreatedDate(DateTime value)
        {
            Value = value;
        }

        public static PortfolioCreatedDate Of(DateTime value)
        {
            return new PortfolioCreatedDate(value);
        }
    }
}