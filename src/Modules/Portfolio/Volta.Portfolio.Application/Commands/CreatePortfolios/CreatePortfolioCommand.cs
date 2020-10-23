using System;
using Volta.BuildingBlocks.Application;

namespace Volta.Portfolios.Application.Commands.CreatePortfolios
{
    public class CreatePortfolioCommand : CommandBase<Guid>
    {
        public CreatePortfolioCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}