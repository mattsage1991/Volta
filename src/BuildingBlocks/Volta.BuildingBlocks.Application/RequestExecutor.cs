using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Volta.BuildingBlocks.Application
{
    public class RequestExecutor : IRequestExecutor
    {
        private readonly IMediator mediator;

        public RequestExecutor(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TResult> ExecuteCommand<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(command, cancellationToken);
        }

        public async Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            return await mediator.Send(query, cancellationToken);
        }
    }
}