using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Application.Commands.CreatePortfolio
{
    public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, Guid>
    {
        private readonly IPortfolioRepository portfolioRepository;

        public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
        {
            this.portfolioRepository = portfolioRepository ?? throw new ArgumentNullException(nameof(portfolioRepository));
        }

        public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var portfolio = Portfolio.Create(PortfolioOwnerId.Of(request.PortfolioOwnerId),
                PortfolioName.Of(request.Name));

            await portfolioRepository.Add(portfolio);

            return portfolio.Id.Value;
        }
    }
}