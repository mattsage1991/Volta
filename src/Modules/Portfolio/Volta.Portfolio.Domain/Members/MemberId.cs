using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Portfolios.Domain.Members
{
    public class MemberId : TypedIdValueBase
    {
        public MemberId(Guid value) : base(value)
        {
        }
    }
}