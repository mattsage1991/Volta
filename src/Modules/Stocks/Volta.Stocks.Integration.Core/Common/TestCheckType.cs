namespace Volta.Stocks.Integration.Core.Common
{
    public class TestCheckType
    {
        public string Type { get; }

        protected TestCheckType(string type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}