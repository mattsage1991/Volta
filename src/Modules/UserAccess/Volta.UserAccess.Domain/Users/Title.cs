using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class Title : ValueObject
    {
        public string Value { get; }

        private Title() { }

        private Title(string value)
        {
            this.Value = value;
        }

        public static Title Of(string value)
        {
            return new Title(value);
        }
    }
}