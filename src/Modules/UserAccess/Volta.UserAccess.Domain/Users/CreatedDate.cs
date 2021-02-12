using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class CreatedDate : ValueObject
    {
        public DateTime Value { get; }

        private CreatedDate() { }

        private CreatedDate(DateTime value)
        {
            this.Value = value;
        }

        public static CreatedDate Of(DateTime value)
        {
            return new CreatedDate(value);
        }
    }
}