using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistrationId : TypedIdValueBase
    {
        public CustomerRegistrationId(Guid value) : base(value)
        {
        }
    }
}