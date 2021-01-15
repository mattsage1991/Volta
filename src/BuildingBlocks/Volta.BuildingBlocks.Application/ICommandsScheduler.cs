using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(InternalCommandBase command);
    }
}