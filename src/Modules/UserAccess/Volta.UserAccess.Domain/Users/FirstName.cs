using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class FirstName : ValueObject
    {
        public string Value { get; }

        private FirstName() { }

        private FirstName(string value)
        {
            this.Value = value;
        }

        public static FirstName Of(string value)
        {
            return new FirstName(value);
        }
    }
}