using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Members
{
    public class MemberId : TypedIdValueBase
    {
        public MemberId(Guid value) : base(value)
        {
        }
    }
}