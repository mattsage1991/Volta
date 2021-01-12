namespace Volta.Stocks.Integration.Core.Common.Interfaces
{
    public interface ITestChecker
    {
        TestCheckType CheckType { get; }
        object GetCheckObject();
    }
}