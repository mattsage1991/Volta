using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Application.Commands.CreatePortfolios
{
    public class CreatePortfolioCommandHandler : ICommandHandler<CreatePortfolioCommand, Guid>
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public CreatePortfolioCommandHandler(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            //var portfolio = new Portfolio(request.Name);

            //await portfolioRepository.Add(portfolio);

            //return portfolio.Id.Value;

            throw new NotImplementedException();
        }
    }
}