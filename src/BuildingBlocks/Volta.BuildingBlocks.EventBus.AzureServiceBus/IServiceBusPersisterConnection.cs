using System;
using Microsoft.Azure.ServiceBus;

namespace Volta.BuildingBlocks.EventBus.AzureServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateModel();
    }
}