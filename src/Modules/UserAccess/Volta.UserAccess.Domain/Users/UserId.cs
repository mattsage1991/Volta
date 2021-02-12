using System;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.UserAccess.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}