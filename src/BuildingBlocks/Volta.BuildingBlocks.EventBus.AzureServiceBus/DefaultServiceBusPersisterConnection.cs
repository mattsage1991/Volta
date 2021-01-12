using System;
using Microsoft.Azure.ServiceBus;

namespace Volta.BuildingBlocks.EventBus.AzureServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder;
        private ITopicClient topicClient;

        bool disposed;

        public DefaultServiceBusPersisterConnection(ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder)
        {
            this.serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ?? 
                throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            this.topicClient = new TopicClient(this.serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => serviceBusConnectionStringBuilder;

        public ITopicClient CreateModel()
        {
            if(topicClient.IsClosedOrClosing)
            {
                topicClient = new TopicClient(serviceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return topicClient;
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;
        }
    }
}
