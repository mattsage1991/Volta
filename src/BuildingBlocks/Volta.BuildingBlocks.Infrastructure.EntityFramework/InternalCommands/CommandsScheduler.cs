using System;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.InternalCommands
{
    public class CommandsScheduler : ICommandsScheduler
    {
        private readonly InternalCommandsContext internalCommandsContext;

        public CommandsScheduler(InternalCommandsContext internalCommandsContext)
        {
            this.internalCommandsContext = internalCommandsContext ?? throw new ArgumentNullException(nameof(internalCommandsContext));
        }

        public Task EnqueueAsync(InternalCommandBase command)
        {
            var internalCommandLogEntry = new InternalCommandLogEntry(command);

            internalCommandsContext.InternalCommandLogs.Add(internalCommandLogEntry);

            return internalCommandsContext.SaveChangesAsync();
        }
    }
}