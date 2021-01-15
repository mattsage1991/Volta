using System;
using Newtonsoft.Json;
using Volta.BuildingBlocks.Application;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.InternalCommands
{
    public class InternalCommandLogEntry
    {
        private InternalCommandLogEntry() { }

        public InternalCommandLogEntry(InternalCommandBase command)
        {
            Id = command.Id;
            EnqueueDate = DateTime.UtcNow;
            Type = command.GetType().FullName;
            Data = JsonConvert.SerializeObject(command);
        }

        public Guid Id { get; private set; }
        public DateTime EnqueueDate { get; private set; }
        public string Type { get; private set; }
        public string Data { get; private set; }
    }
}