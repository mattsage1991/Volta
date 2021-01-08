using System.Threading.Tasks;

namespace Volta.Stocks.Integration.Core.Common.Interfaces
{
    public interface ITestTeardown
    {
        Task TeardownAsync();
    }
}