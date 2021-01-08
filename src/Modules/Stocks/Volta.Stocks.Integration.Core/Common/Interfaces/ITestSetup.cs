using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Volta.Stocks.Integration.Core.Common.Interfaces
{
    public interface ITestSetup
    {
        Task SetupAsync(IServiceCollection services);
    }
}