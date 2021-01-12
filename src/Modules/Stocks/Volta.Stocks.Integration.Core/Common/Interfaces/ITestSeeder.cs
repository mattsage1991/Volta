using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volta.Stocks.Integration.Core.Common.Interfaces
{
    public interface ITestSeeder
    {
        TestSeedType SeedType { get; }
        Task SeedAsync(List<object> seedData);
    }
}