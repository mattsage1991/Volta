using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class MemberId : ValueObject
    {
        public Guid Value { get; }

        private MemberId() { }
        private MemberId(Guid value)
        {
            Value = value;
        }

        public static MemberId Of(Guid value)
        {
            return new MemberId(value);
        }
    }
}