using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Portfolios.Application.Commands.CreatePortfolio
{
    public class CreatePortfolioCommand : ICommand<Guid>
    {
        public Guid PortfolioOwnerId { get; set; }
        public string Name { get; }

        public CreatePortfolioCommand(Guid portfolioOwnerId, string name)
        {
            Name = name;
            PortfolioOwnerId = portfolioOwnerId;
        }
    }
}