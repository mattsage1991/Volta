using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.PortfolioOwners
{
    public class CreatedDate : ValueObject
    {
        public DateTime Value { get; }

        private CreatedDate() { }
        private CreatedDate(DateTime value)
        {
            Value = value;
        }

        public static CreatedDate Of(DateTime value)
        {
            return new CreatedDate(value);
        }
    }
}