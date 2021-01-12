namespace Volta.Stocks.Integration.Core.Common
{
    public class TestSeedType
    {
        public string Type { get; }
        
        protected TestSeedType(string type)
        {
            Type = type;
        }
        
        public override string ToString()
        {
            return Type;
        }
    }
}