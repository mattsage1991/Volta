using System.Threading.Tasks;
using Autofac;

namespace Volta.Stocks.Integration.Core.Common.Interfaces
{
    public interface ITestContainer
    {
        Task SetupContainerAsync(ContainerBuilder builder);
    }
}