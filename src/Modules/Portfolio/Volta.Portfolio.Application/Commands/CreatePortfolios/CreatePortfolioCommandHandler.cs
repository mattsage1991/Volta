using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Application.Commands.CreatePortfolios
{
    public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, Guid>
    {
        private readonly IPortfolioRepository portfolioRepository;

        public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
        {
            this.portfolioRepository = portfolioRepository;
        }

        public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var portfolio = Portfolio.CreateNew(request.Name);

            await portfolioRepository.Add(portfolio);

            return portfolio.Id.Value;
        }
    }
}