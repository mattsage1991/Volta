using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}