using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Infrastructure.EventBus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}