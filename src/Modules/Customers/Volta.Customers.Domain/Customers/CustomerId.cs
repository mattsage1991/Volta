using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Customers.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}