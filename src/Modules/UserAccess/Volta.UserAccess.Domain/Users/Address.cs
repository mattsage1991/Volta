using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class Address : ValueObject
    {
        public string Value { get; }

        private Address() { }

        private Address(string value)
        {
            this.Value = value;
        }

        public static Address Of(string value)
        {
            return new Address(value);
        }
    }
}