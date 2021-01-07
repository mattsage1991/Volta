using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistrationId : TypedIdValueBase
    {
        public CustomerRegistrationId(Guid value) : base(value)
        {
        }
    }
}