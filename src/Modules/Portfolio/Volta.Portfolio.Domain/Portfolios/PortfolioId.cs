using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioId
    {
        private readonly Guid _value;

        public PortfolioId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Portfolio id cannot be empty");

            _value = value;
        }
    }
}
