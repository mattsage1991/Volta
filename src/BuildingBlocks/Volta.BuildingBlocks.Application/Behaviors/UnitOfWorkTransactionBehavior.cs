using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application.Behaviors
{
    public class UnitOfWorkTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly ILogger<TRequest> logger;
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkTransactionBehavior(ILogger<TRequest> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;

            logger.LogInformation("Begin Transaction: {Name} {@Request}", name, request);

            var response = await next();

            logger.LogInformation("Completing Transaction: {Name} {@Request}", name, request);

            await unitOfWork.Complete(cancellationToken);

            logger.LogInformation("Completed Transaction: {Name} {@Request}", name, request);

            return response;
        }
    }
}
