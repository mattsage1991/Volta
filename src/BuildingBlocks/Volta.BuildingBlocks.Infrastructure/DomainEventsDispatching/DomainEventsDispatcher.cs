using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator mediator;
        private readonly IDomainEventsAccessor domainEventsProvider;

        public DomainEventsDispatcher(
            IMediator mediator,
            IDomainEventsAccessor domainEventsProvider)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
            this.domainEventsProvider = domainEventsProvider ?? throw new ArgumentNullException(nameof(domainEventsProvider)); ;
        }

        public async Task DispatchEvents(CancellationToken cancellationToken = default)
        {
            while (domainEventsProvider.GetAllDomainEvents().Any())
            {
                var domainEvents = domainEventsProvider.GetAllDomainEvents();

                domainEventsProvider.ClearAllDomainEvents();

                var tasks = domainEvents
                    .Select(async (domainEvent) =>
                    {
                        await mediator.Publish(domainEvent, cancellationToken);
                    });

                await Task.WhenAll(tasks);
            }
        }
    }
}
