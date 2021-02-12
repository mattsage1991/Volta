using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class LastName : ValueObject
    {
        public string Value { get; }

        private LastName() { }

        private LastName(string value)
        {
            this.Value = value;
        }

        public static LastName Of(string value)
        {
            return new LastName(value);
        }
    }
}